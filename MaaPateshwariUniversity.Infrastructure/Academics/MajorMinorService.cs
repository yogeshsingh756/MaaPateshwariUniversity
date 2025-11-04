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
    public class MajorMinorService : IMajorMinorService
    {
        private readonly ApplicationDbContext _db;
        public MajorMinorService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<MajorMinorMapping>>> GetByMajorAsync(long majorProgramId)
        {
            try
            {
                var list = await _db.MajorMinorMappings
                    .Where(x => x.MajorProgramId == majorProgramId && x.IsActive)
                    .OrderBy(x => x.MinorProgramId)
                    .ToListAsync();
                return ApiResponse<List<MajorMinorMapping>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<MajorMinorMapping>>.Fail("Failed to fetch major–minor", ex.Message);
            }
        }

        public async Task<ApiResponse<MajorMinorMapping>> UpsertAsync(MajorMinorMapping model)
        {
            try
            {
                if (!await _db.ProgramMasters.AnyAsync(p => p.ProgramId == model.MajorProgramId && p.IsActive))
                    return ApiResponse<MajorMinorMapping>.Fail("Invalid MajorProgramId");
                if (!await _db.ProgramMasters.AnyAsync(p => p.ProgramId == model.MinorProgramId && p.IsActive))
                    return ApiResponse<MajorMinorMapping>.Fail("Invalid MinorProgramId");

                if (model.MajorMinorId == 0) _db.MajorMinorMappings.Add(model);
                else _db.MajorMinorMappings.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<MajorMinorMapping>.Ok(model, "Saved");
            }
            catch (DbUpdateException dbex)
            {
                return ApiResponse<MajorMinorMapping>.Fail("Duplicate major/minor?", dbex.InnerException?.Message ?? dbex.Message);
            }
            catch (Exception ex)
            {
                return ApiResponse<MajorMinorMapping>.Fail("Failed to save major–minor", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(long majorMinorId)
        {
            try
            {
                var row = await _db.MajorMinorMappings.FindAsync(majorMinorId);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                row.IsActive = false;
                _db.MajorMinorMappings.Update(row);
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
