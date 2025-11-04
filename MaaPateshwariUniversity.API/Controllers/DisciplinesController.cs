using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/disciplines")]
    [Authorize]
    public class DisciplinesController : ControllerBase
    {
        private readonly IDisciplineService _svc;
        public DisciplinesController(IDisciplineService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] short? streamId) => Ok(await _svc.GetAllAsync(streamId));

        [HttpPost]
        public async Task<IActionResult> Upsert(DisciplineMaster model) => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id) => Ok(await _svc.SoftDeleteAsync(id));
    }
}
