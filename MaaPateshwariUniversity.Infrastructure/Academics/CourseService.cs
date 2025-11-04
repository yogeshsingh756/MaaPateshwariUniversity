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
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _db;
        public CourseService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<CourseMaster>>> GetAllAsync(string? q)
        {
            try
            {
                var qu = _db.CourseMasters.AsQueryable().Where(x => x.IsActive);
                if (!string.IsNullOrWhiteSpace(q))
                    qu = qu.Where(x => x.CourseName.Contains(q) || x.CourseCode.Contains(q));
                var list = await qu.OrderBy(x => x.CourseName).ToListAsync();
                return ApiResponse<List<CourseMaster>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<CourseMaster>>.Fail("Failed to fetch courses", ex.Message);
            }
        }

        public async Task<ApiResponse<CourseMaster>> UpsertAsync(CourseMaster model)
        {
            try
            {
                if (model.CourseId == 0) _db.CourseMasters.Add(model);
                else _db.CourseMasters.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<CourseMaster>.Ok(model, "Saved");
            }
            catch (DbUpdateException dbex)
            {
                return ApiResponse<CourseMaster>.Fail("Duplicate CourseCode?", dbex.InnerException?.Message ?? dbex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<CourseMaster>.Fail("Failed to save course", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var row = await _db.CourseMasters.FindAsync(id);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                row.IsActive = false;
                _db.CourseMasters.Update(row);
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
