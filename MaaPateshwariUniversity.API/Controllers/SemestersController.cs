using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/semesters")]
    [Authorize]
    public class SemestersController : ControllerBase
    {
        private readonly ISemesterService _svc;
        public SemestersController(ISemesterService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Upsert(SemesterMaster model) => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id) => Ok(await _svc.DeleteAsync(id));
    }
}
