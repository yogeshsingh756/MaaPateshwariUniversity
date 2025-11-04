using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/major-minor")]
    [Authorize]
    public class MajorMinorController : ControllerBase
    {
        private readonly IMajorMinorService _svc;
        public MajorMinorController(IMajorMinorService svc) => _svc = svc;

        [HttpGet("{majorProgramId:long}")]
        public async Task<IActionResult> GetByMajor(long majorProgramId)
            => Ok(await _svc.GetByMajorAsync(majorProgramId));

        [HttpPost]
        public async Task<IActionResult> Upsert(MajorMinorMapping model)
            => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{majorMinorId:long}")]
        public async Task<IActionResult> Delete(long majorMinorId)
            => Ok(await _svc.DeleteAsync(majorMinorId));
    }
}
