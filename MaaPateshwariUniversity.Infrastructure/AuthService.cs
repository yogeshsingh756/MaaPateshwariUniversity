using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MaaPateshwariUniversity.Application.DTOs;
using MaaPateshwariUniversity.Application.Interfaces;
using MaaPateshwariUniversity.Domain.Entities;
using System.Threading.Tasks;
using System;

namespace MaaPateshwariUniversity.Infrastructure;
public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    private readonly IConfiguration _cfg;
    public AuthService(ApplicationDbContext db, IConfiguration cfg){ _db=db; _cfg=cfg; }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Username && u.IsActive);
        if (user == null) return new LoginResponse(false, null, null, null, 0);
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return new LoginResponse(false, null, null, null, 0);
        var role = await _db.Roles.FindAsync(user.RoleId);
        var token = GenerateJwt(user, role?.Name ?? "User", out var expiresIn);
        var refresh = await CreateRefreshTokenAsync(user.UserId);
        return new LoginResponse(true, token, refresh, role?.Name, expiresIn);
    }

    public async Task<string?> RefreshAsync(RefreshRequest request)
    {
        var rt = await _db.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken && !r.Revoked && r.ExpiresAt > DateTime.UtcNow);
        if (rt == null) return null;
        var user = await _db.Users.FindAsync(rt.UserId);
        if (user == null) return null;
        var role = await _db.Roles.FindAsync(user.RoleId);
        var token = GenerateJwt(user, role?.Name ?? "User", out var _);
        return token;
    }

    private string GenerateJwt(User user, string role, out int expiresIn)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_cfg["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        expiresIn = 3600;
        var token = new JwtSecurityToken(
            claims: new[] { new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()), new Claim(ClaimTypes.Email, user.Email), new Claim(ClaimTypes.Role, role) },
            expires: DateTime.UtcNow.AddSeconds(expiresIn),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<string> CreateRefreshTokenAsync(long userId)
    {
        var token = Guid.NewGuid().ToString("N");
        _db.RefreshTokens.Add(new RefreshToken{ UserId=userId, Token=token, ExpiresAt=DateTime.UtcNow.AddDays(7), Revoked=false });
        await _db.SaveChangesAsync();
        return token;
    }
}
