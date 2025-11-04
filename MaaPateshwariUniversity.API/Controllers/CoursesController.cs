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
    [Route("api/academics/courses")]
    [Authorize]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseService _svc;
        private readonly ApplicationDbContext _db;
        public CoursesController(ICourseService svc, ApplicationDbContext applicationDbContext) { _svc = svc; _db = applicationDbContext; }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? q) => Ok(await _svc.GetAllAsync(q));

        [HttpPost]
        public async Task<IActionResult> Upsert(CourseMaster model) => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id) => Ok(await _svc.DeleteAsync(id));
        [HttpGet("lookup")]
        public async Task<IActionResult> CourseLookup([FromQuery] string? q, [FromQuery] int take = 20)
        {
            try
            {
                var qu = _db.CourseMasters.AsQueryable().Where(x => x.IsActive);
                if (!string.IsNullOrWhiteSpace(q))
                    qu = qu.Where(x => x.CourseName.Contains(q) || x.CourseCode.Contains(q));

                var list = await qu.OrderBy(x => x.CourseName).Take(Math.Clamp(take, 1, 50)).ToListAsync();
                return Ok(ApiResponse<object>.Ok(list.Select(x => new { x.CourseId, x.CourseCode, x.CourseName, x.Credit })));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.Fail("Lookup failed", ex.Message));
            }
        }
    }
}
