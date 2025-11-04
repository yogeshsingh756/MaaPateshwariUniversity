using MaaPateshwariUniversity.Application.DTOs;
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
    public class OrganizationProgramService : IOrganizationProgramService
    {
        private readonly ApplicationDbContext _db;
        public OrganizationProgramService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<ProgramSyllabusDto>>> GetByOrganizationAsync(long organizationId)
        {
            try
            {
                var rawData = await (from orgProg in _db.OrganizationPrograms
                                     join prog in _db.ProgramMasters on orgProg.ProgramId equals prog.ProgramId
                                     join pcm in _db.ProgramCourseMappings on prog.ProgramId equals pcm.ProgramId
                                     join course in _db.CourseMasters on pcm.CourseId equals course.CourseId
                                     join sem in _db.SemesterMasters on pcm.SemesterId equals sem.SemesterId
                                     join comp in _db.NEPComponentTypes on pcm.ComponentTypeId equals comp.ComponentTypeId
                                     where orgProg.OrganizationId == organizationId && orgProg.IsActive
                                     select new
                                     {
                                         prog.ProgramId,
                                         prog.ProgramName,
                                         prog.DegreeLevel,
                                         prog.DurationYears,
                                         prog.TotalCredits,
                                         pcm.SemesterId,
                                         SemesterName = sem.SemesterName,
                                         course.CourseId,
                                         course.CourseCode,
                                         course.CourseName,
                                         course.Credit,
                                         Component = comp.ComponentName
                                     }).ToListAsync();

                var result = rawData
                    .GroupBy(p => new { p.ProgramId, p.ProgramName, p.DegreeLevel, p.DurationYears, p.TotalCredits })
                    .Select(p => new ProgramSyllabusDto
                    {
                        ProgramId = p.Key.ProgramId,
                        ProgramName = p.Key.ProgramName,
                        DegreeLevel = p.Key.DegreeLevel,
                        DurationYears = p.Key.DurationYears,
                        TotalCredits = Convert.ToInt32(p.Sum(x => x.Credit)),
                        Semesters = p.GroupBy(s => new { s.SemesterId, s.SemesterName })
                                     .Select(s => new SemesterDto
                                     {
                                         SemesterId = s.Key.SemesterId,
                                         SemesterName = s.Key.SemesterName,
                                         Courses = s.Select(c => new CourseDto
                                         {
                                             CourseId = c.CourseId,
                                             CourseCode = c.CourseCode,
                                             CourseName = c.CourseName,
                                             Credit = c.Credit,
                                             Component = c.Component
                                         }).ToList()
                                     }).ToList()
                    }).ToList();

                return ApiResponse<List<ProgramSyllabusDto>>.Ok(result);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProgramSyllabusDto>>.Fail("Error fetching grouped syllabus", ex.Message);
            }
        }



        public async Task<ApiResponse<OrganizationProgram>> UpsertAsync(OrganizationProgram model)
        {
            try
            {
                if (!await _db.Organizations.AnyAsync(o => o.OrganizationId == model.OrganizationId))
                    return ApiResponse<OrganizationProgram>.Fail("Invalid OrganizationId");
                if (!await _db.ProgramMasters.AnyAsync(p => p.ProgramId == model.ProgramId && p.IsActive))
                    return ApiResponse<OrganizationProgram>.Fail("Invalid ProgramId");

                if (model.OrganizationProgramId == 0) _db.OrganizationPrograms.Add(model);
                else _db.OrganizationPrograms.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<OrganizationProgram>.Ok(model, "Saved");
            }
            catch (DbUpdateException dbex)
            {
                return ApiResponse<OrganizationProgram>.Fail("Duplicate (OrganizationId, ProgramId)?", dbex.InnerException?.Message ?? dbex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<OrganizationProgram>.Fail("Failed to save organization program", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long organizationProgramId)
        {
            try
            {
                var row = await _db.OrganizationPrograms.FindAsync(organizationProgramId);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                row.IsActive = false;
                _db.OrganizationPrograms.Update(row);
                await _db.SaveChangesAsync();
                return ApiResponse<bool>.Ok(true, "Deleted");
            }
            catch (Exception ex)
            {
                return ApiResponse<bool>.Fail("Failed to delete", ex.Message);
            }
        }
    }
}
