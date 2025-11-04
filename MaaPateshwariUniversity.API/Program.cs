using MaaPateshwariUniversity.Application.Interfaces;
using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Domain.Entities;
using MaaPateshwariUniversity.Infrastructure;
using MaaPateshwariUniversity.Infrastructure.Academics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var cs = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseMySql(cs!, ServerVersion.AutoDetect(cs!)));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IOrganizationService, OrganizationService>();
builder.Services.AddScoped<IStreamService, StreamService>();
builder.Services.AddScoped<IDisciplineService, DisciplineService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ISemesterService, SemesterService>();
builder.Services.AddScoped<INEPComponentService, NEPComponentService>();
builder.Services.AddScoped<IProgramCourseService, ProgramCourseService>();
builder.Services.AddScoped<IOrganizationProgramService, OrganizationProgramService>();
builder.Services.AddScoped<IMajorMinorService, MajorMinorService>();

var key = builder.Configuration["Jwt:Key"] ?? "supersecret_development_key_change_me";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer=false, ValidateAudience=false, ValidateIssuerSigningKey=true,
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    });
builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{ Title="Ma Pateshwari University API", Version="v1"});
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme{
        Name="Authorization", Type=SecuritySchemeType.ApiKey, Scheme="Bearer", BearerFormat="JWT", In=ParameterLocation.Header, Description="Enter: Bearer {token}"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement{{ new OpenApiSecurityScheme{ Reference=new OpenApiReference{ Type=ReferenceType.SecurityScheme, Id="Bearer"}}, new string[]{} }});
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});
var app = builder.Build();
// Add OPTIONS handling middleware

// ensure uploads folder
var uploadRoot = Path.Combine(app.Environment.ContentRootPath, "wwwroot", "uploads", "certificates");
Directory.CreateDirectory(uploadRoot);

using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    try { db.Database.Migrate(); } catch { db.Database.EnsureCreated(); }
    if (!db.Users.Any(u=>u.Email=="admin@mpu.ac.in"))
    {
        db.Users.Add(new User{ UserId=1, Email="admin@mpu.ac.in", PasswordHash=BCrypt.Net.BCrypt.HashPassword("Admin@123"), RoleId=1, IsActive=true });
        await db.SaveChangesAsync();
    }
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
