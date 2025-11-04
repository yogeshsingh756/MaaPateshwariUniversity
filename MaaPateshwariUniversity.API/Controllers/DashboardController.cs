using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaaPateshwariUniversity.Infrastructure;

namespace MaaPateshwariUniversity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public DashboardController(ApplicationDbContext db){ _db = db; }

    [HttpGet("summary")]
    [Authorize]
    public async Task<IActionResult> Summary()
    {
        var totalOrgs = await _db.Organizations.CountAsync();
        var universities = await _db.Organizations.CountAsync(x => x.OrganizationTypeId == 1);
        var colleges = await _db.Organizations.CountAsync(x => x.OrganizationTypeId == 2);
        var accCount = await _db.OrganizationAccreditations.CountAsync();
        var published = await _db.Organizations.CountAsync(x => x.StatusId == 1);
        var draft = await _db.Organizations.CountAsync(x => x.StatusId == 2);
        return Ok(new { totalOrgs, universities, colleges, accreditations = accCount, published, draft });
    }
}
