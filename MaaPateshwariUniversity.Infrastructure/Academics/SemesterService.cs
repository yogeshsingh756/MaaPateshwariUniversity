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
    public class SemesterService : ISemesterService
    {
        private readonly ApplicationDbContext _db;
        public SemesterService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<SemesterMaster>>> GetAllAsync()
        {
            try
            {
                var list = await _db.SemesterMasters.OrderBy(x => x.SemesterId).ToListAsync();
                return ApiResponse<List<SemesterMaster>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<SemesterMaster>>.Fail("Failed to fetch semesters", ex.Message);
            }
        }

        public async Task<ApiResponse<SemesterMaster>> UpsertAsync(SemesterMaster model)
        {
            try
            {
                if (model.SemesterId == 0) _db.SemesterMasters.Add(model);
                else _db.SemesterMasters.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<SemesterMaster>.Ok(model, "Saved");
            }
            catch (Exception ex)
            {
                return ApiResponse<SemesterMaster>.Fail("Failed to save semester", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(byte id)
        {
            try
            {
                var row = await _db.SemesterMasters.FindAsync(id);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                _db.SemesterMasters.Remove(row); // hard delete is ok here
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
