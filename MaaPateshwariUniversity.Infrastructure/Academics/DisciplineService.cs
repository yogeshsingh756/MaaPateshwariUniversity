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
    public class DisciplineService : IDisciplineService
    {
        private readonly ApplicationDbContext _db;
        public DisciplineService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<DisciplineMaster>>> GetAllAsync(short? streamId)
        {
            try
            {
                var q = _db.DisciplineMasters.AsQueryable().Where(x => x.IsActive);
                if (streamId.HasValue && streamId.Value > 0) q = q.Where(x => x.StreamId == streamId);
                var list = await q.OrderBy(x => x.DisciplineName).ToListAsync();
                return ApiResponse<List<DisciplineMaster>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<DisciplineMaster>>.Fail("Failed to fetch disciplines", ex.Message);
            }
        }

        public async Task<ApiResponse<DisciplineMaster>> UpsertAsync(DisciplineMaster model)
        {
            try
            {
                // validate stream exists
                if (!await _db.StreamMasters.AnyAsync(s => s.StreamId == model.StreamId && s.IsActive))
                    return ApiResponse<DisciplineMaster>.Fail("Invalid StreamId");

                if (model.DisciplineId == 0) _db.DisciplineMasters.Add(model);
                else _db.DisciplineMasters.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<DisciplineMaster>.Ok(model, "Saved");
            }
            catch (Exception ex)
            {
                return ApiResponse<DisciplineMaster>.Fail("Failed to save discipline", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> SoftDeleteAsync(short id)
        {
            try
            {
                var row = await _db.DisciplineMasters.FindAsync(id);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                row.IsActive = false;
                _db.DisciplineMasters.Update(row);
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
