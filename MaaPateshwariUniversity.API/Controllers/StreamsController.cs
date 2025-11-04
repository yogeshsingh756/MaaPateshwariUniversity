using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/streams")]
    [Authorize]
    public class StreamsController : ControllerBase
    {
        private readonly IStreamService _svc;
        public StreamsController(IStreamService svc) => _svc = svc;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _svc.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Upsert(StreamMaster model) => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(short id) => Ok(await _svc.SoftDeleteAsync(id));
    }
}
