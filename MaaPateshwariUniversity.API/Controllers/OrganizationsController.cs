using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MaaPateshwariUniversity.Application.Interfaces;
using MaaPateshwariUniversity.Domain.Entities;

namespace MaaPateshwariUniversity.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _svc;
    private long? CurrentUserId => long.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : null;
    public OrganizationsController(IOrganizationService svc){ _svc = svc; }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] string? q, [FromQuery] byte? typeId, [FromQuery] ushort? stateId, [FromQuery] uint? districtId, [FromQuery] byte? statusId, [FromQuery] bool? visible, [FromQuery] int page=1, [FromQuery] int pageSize=20)
    {
        page = Math.Max(page,1); pageSize = Math.Clamp(pageSize,1,200);
        var (items,total) = await _svc.SearchAsync(q, typeId, stateId, districtId, statusId, visible, page, pageSize);
        Response.Headers["X-Total-Count"] = total.ToString();
        Response.Headers["X-Page"] = page.ToString();
        Response.Headers["X-Page-Size"] = pageSize.ToString();
        return Ok(items);
    }

    [HttpGet("{id:long}")]
    [Authorize]
    public async Task<IActionResult> Get(long id) => Ok(await _svc.GetAsync(id));

    [HttpPost]
    [Authorize(Roles="SuperAdmin,UniversityAdmin")]
    public async Task<IActionResult> Upsert(Organization org) => Ok(await _svc.UpsertAsync(org, CurrentUserId));

    [HttpDelete("{id:long}")]
    [Authorize(Roles="SuperAdmin")]
    public async Task<IActionResult> Delete(long id) => Ok(await _svc.SoftDeleteAsync(id, CurrentUserId));
}
