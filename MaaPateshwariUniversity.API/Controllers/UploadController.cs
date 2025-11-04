using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaaPateshwariUniversity.Infrastructure;

namespace MaaPateshwariUniversity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UploadController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _env;
    public UploadController(ApplicationDbContext db, IWebHostEnvironment env){ _db = db; _env = env; }

    [HttpPost("organization/{organizationId:long}/certificate")]
    [Authorize(Roles="SuperAdmin,UniversityAdmin")]
    [RequestSizeLimit(25_000_000)]
    public async Task<IActionResult> UploadCertificate([FromRoute] long organizationId, [FromQuery] int agencyId, IFormFile file, [FromQuery] string? grade=null, [FromQuery] DateTime? validFrom=null, [FromQuery] DateTime? validTo=null)
    {
        var orgExists = await _db.Organizations.AnyAsync(x => x.OrganizationId == organizationId);
        if (!orgExists) return NotFound(new { message = "Organization not found" });
        if (file == null || file.Length == 0) return BadRequest(new { message = "No file uploaded" });
        if (!file.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)) return BadRequest(new { message = "Only PDF files allowed" });

        var uploadsPath = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "certificates", organizationId.ToString());
        Directory.CreateDirectory(uploadsPath);
        var safeName = $"{Guid.NewGuid():N}.pdf";
        var fullPath = Path.Combine(uploadsPath, safeName);
        using (var fs = System.IO.File.Create(fullPath)) { await file.CopyToAsync(fs); }

        var relativeUrl = $"/uploads/certificates/{organizationId}/{safeName}";
        _db.OrganizationAccreditations.Add(new MaaPateshwariUniversity.Domain.Entities.OrganizationAccreditation {
            OrganizationId = organizationId, AgencyId = agencyId, GradeOrStatus = grade, ValidFrom = validFrom, ValidTo = validTo, CertificateUrl = relativeUrl
        });
        await _db.SaveChangesAsync();
        return Ok(new { message = "Uploaded", url = relativeUrl });
    }
}
