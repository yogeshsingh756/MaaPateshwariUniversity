using MaaPateshwariUniversity.Application.Interfaces.Academics;
using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Infrastructure.Academics
{
    public class ProgramCourseService : IProgramCourseService
    {
        private readonly ApplicationDbContext _db;
        public ProgramCourseService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<ProgramCourseMapping>>> GetByProgramAsync(long programId)
        {
            try
            {
                var list = await _db.ProgramCourseMappings
                    .Where(x => x.ProgramId == programId)
                    .OrderBy(x => x.SemesterId)
                    .ThenBy(x => x.ProgramCourseId)
                    .ToListAsync();

                return ApiResponse<List<ProgramCourseMapping>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProgramCourseMapping>>.Fail("Failed to fetch mapping", ex.Message);
            }
        }

        public async Task<ApiResponse<ProgramCourseMapping>> UpsertAsync(ProgramCourseMapping model)
        {
            try
            {
                // minimal validation
                if (!await _db.ProgramMasters.AnyAsync(p => p.ProgramId == model.ProgramId && p.IsActive))
                    return ApiResponse<ProgramCourseMapping>.Fail("Invalid ProgramId");
                if (!await _db.CourseMasters.AnyAsync(c => c.CourseId == model.CourseId && c.IsActive))
                    return ApiResponse<ProgramCourseMapping>.Fail("Invalid CourseId");
                if (!await _db.SemesterMasters.AnyAsync(s => s.SemesterId == model.SemesterId))
                    return ApiResponse<ProgramCourseMapping>.Fail("Invalid SemesterId");
                if (!await _db.NEPComponentTypes.AnyAsync(n => n.ComponentTypeId == model.ComponentTypeId))
                    return ApiResponse<ProgramCourseMapping>.Fail("Invalid ComponentTypeId");

                if (model.ProgramCourseId == 0) _db.ProgramCourseMappings.Add(model);
                else _db.ProgramCourseMappings.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<ProgramCourseMapping>.Ok(model, "Saved");
            }
            catch (DbUpdateException dbex)
            {
                return ApiResponse<ProgramCourseMapping>.Fail("Duplicate mapping? (Program, Course, Semester must be unique)", dbex.InnerException?.Message ?? dbex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<ProgramCourseMapping>.Fail("Failed to save mapping", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long programCourseId)
        {
            try
            {
                var row = await _db.ProgramCourseMappings.FindAsync(programCourseId);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                _db.ProgramCourseMappings.Remove(row);
                await _db.SaveChangesAsync();
                return ApiResponse<bool>.Ok(true, "Deleted");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail("Failed to delete mapping", ex.Message);
            }
        }
    }
}
