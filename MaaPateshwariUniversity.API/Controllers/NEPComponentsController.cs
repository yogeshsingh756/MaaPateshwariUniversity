using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/nep-components")]
    [Authorize]
    public class NEPComponentsController : ControllerBase
    {
        private readonly INEPComponentService _svc;
        public NEPComponentsController(INEPComponentService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Upsert(NEPComponentType model) => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(byte id) => Ok(await _svc.DeleteAsync(id));
    }
}
