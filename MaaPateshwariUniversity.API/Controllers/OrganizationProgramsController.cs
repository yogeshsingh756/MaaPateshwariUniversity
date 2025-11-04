using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/organization-programs")]
    [Authorize]
    public class OrganizationProgramsController : ControllerBase
    {
        private readonly IOrganizationProgramService _svc;
        public OrganizationProgramsController(IOrganizationProgramService svc) => _svc = svc;

        [HttpGet("{organizationId:long}")]
        public async Task<IActionResult> GetByOrganization(long organizationId)
            => Ok(await _svc.GetByOrganizationAsync(organizationId));

        [HttpPost]
        public async Task<IActionResult> Upsert(OrganizationProgram model)
            => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{organizationProgramId:long}")]
        public async Task<IActionResult> Delete(long organizationProgramId)
            => Ok(await _svc.DeleteAsync(organizationProgramId));
    }
}
