using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using MaaPateshwariUniversity.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/programs")]
    [Authorize]
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramService _svc;
        private readonly ApplicationDbContext _db;
        public ProgramsController(IProgramService svc, ApplicationDbContext applicationDbContext) { _svc = svc; _db = applicationDbContext; }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] short? disciplineId) => Ok(await _svc.GetAllAsync(disciplineId));

        [HttpPost]
        public async Task<IActionResult> Upsert(ProgramMaster model) => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) => Ok(await _svc.DeleteAsync(id));
        [HttpGet("search")]
        public async Task<IActionResult> SearchPrograms([FromQuery] short? disciplineId, [FromQuery] string? q, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                page = Math.Max(1, page); pageSize = Math.Clamp(pageSize, 1, 100);
                var qu = _db.ProgramMasters.AsQueryable().Where(x => x.IsActive);
                if (disciplineId.HasValue) qu = qu.Where(x => x.DisciplineId == disciplineId);
                if (!string.IsNullOrWhiteSpace(q)) qu = qu.Where(x => x.ProgramName.Contains(q));

                var total = await qu.CountAsync();
                var items = await qu.OrderBy(x => x.ProgramName).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

                Response.Headers["X-Total-Count"] = total.ToString();
                return Ok(ApiResponse<object>.Ok(items));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.Fail("Search failed", ex.Message));
            }
        }
    }
}
