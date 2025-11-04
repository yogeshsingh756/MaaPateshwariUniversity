using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MaaPateshwariUniversity.API.Controllers
{
    [ApiController]
    [Route("api/academics/bootstrap")]
    [Authorize]
    public class AcademicsBootstrapController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public AcademicsBootstrapController(ApplicationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var streams = await _db.StreamMasters.Where(x => x.IsActive).ToListAsync();
                var disciplines = await _db.DisciplineMasters.Where(x => x.IsActive).ToListAsync();
                var semesters = await _db.SemesterMasters.ToListAsync();
                var nep = await _db.NEPComponentTypes.ToListAsync();

                return Ok(ApiResponse<object>.Ok(new { streams, disciplines, semesters, nep }));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.Fail("Bootstrap load failed", ex.Message));
            }
        }
    }
}
