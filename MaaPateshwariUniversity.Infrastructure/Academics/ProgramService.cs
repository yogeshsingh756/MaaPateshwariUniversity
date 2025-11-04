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
    public class ProgramService : IProgramService
    {
        private readonly ApplicationDbContext _db;
        public ProgramService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<ProgramMaster>>> GetAllAsync(short? disciplineId)
        {
            try
            {
                var q = _db.ProgramMasters.AsQueryable().Where(x => x.IsActive);
                if (disciplineId.HasValue && disciplineId.Value > 0) q = q.Where(x => x.DisciplineId == disciplineId);
                var list = await q.OrderBy(x => x.ProgramName).ToListAsync();
                return ApiResponse<List<ProgramMaster>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<ProgramMaster>>.Fail("Failed to fetch programs", ex.Message);
            }
        }

        public async Task<ApiResponse<ProgramMaster>> UpsertAsync(ProgramMaster model)
        {
            try
            {
                if (!await _db.DisciplineMasters.AnyAsync(d => d.DisciplineId == model.DisciplineId && d.IsActive))
                    return ApiResponse<ProgramMaster>.Fail("Invalid DisciplineId");

                if (model.ProgramId == 0) _db.ProgramMasters.Add(model);
                else _db.ProgramMasters.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<ProgramMaster>.Ok(model, "Saved");
            }
            catch (Exception ex)
            {
                return ApiResponse<ProgramMaster>.Fail("Failed to save program", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long id)
        {
            try
            {
                var row = await _db.ProgramMasters.FindAsync(id);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                row.IsActive = false;
                _db.ProgramMasters.Update(row);
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
