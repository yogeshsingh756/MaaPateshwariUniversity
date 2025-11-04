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
    [Route("api/academics/program-courses")]
    [Authorize]
    public class ProgramCoursesController : ControllerBase
    {
        private readonly IProgramCourseService _svc;
        private readonly ApplicationDbContext _db;
        public ProgramCoursesController(IProgramCourseService svc, ApplicationDbContext applicationDbContext)
        { _svc = svc;
           _db = applicationDbContext ;
        }

        [HttpGet("{programId:long}")]
        public async Task<IActionResult> GetByProgram(long programId) => Ok(await _svc.GetByProgramAsync(programId));

        [HttpPost]
        public async Task<IActionResult> Upsert(ProgramCourseMapping model) => Ok(await _svc.UpsertAsync(model));

        [HttpDelete("{programCourseId:long}")]
        public async Task<IActionResult> Delete(long programCourseId) => Ok(await _svc.DeleteAsync(programCourseId));

        [HttpGet("programs/{programId:long}/syllabus")]
        public async Task<IActionResult> GetProgramSyllabus(long programId)
        {
            try
            {
                var data = await (from pcm in _db.ProgramCourseMappings
                                  join c in _db.CourseMasters on pcm.CourseId equals c.CourseId
                                  join sem in _db.SemesterMasters on pcm.SemesterId equals sem.SemesterId
                                  join comp in _db.NEPComponentTypes on pcm.ComponentTypeId equals comp.ComponentTypeId
                                  where pcm.ProgramId == programId
                                  orderby pcm.SemesterId, c.CourseName
                                  select new
                                  {
                                      pcm.ProgramCourseId,
                                      pcm.ProgramId,
                                      pcm.SemesterId,
                                      Semester = sem.SemesterName,
                                      pcm.ComponentTypeId,
                                      Component = comp.ComponentName,
                                      pcm.CourseId,
                                      c.CourseCode,
                                      c.CourseName,
                                      c.Credit,
                                      pcm.IsMandatory
                                  }).ToListAsync();

                return Ok(ApiResponse<object>.Ok(data));
            }
            catch (Exception ex)
            {
                return Ok(ApiResponse<object>.Fail("Failed to fetch syllabus", ex.Message));
            }
        }
    }
}
