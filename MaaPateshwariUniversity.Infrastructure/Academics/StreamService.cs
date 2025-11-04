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
    public class StreamService : IStreamService
    {
        private readonly ApplicationDbContext _db;
        public StreamService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<StreamMaster>>> GetAllAsync()
        {
            try
            {
                var list = await _db.StreamMasters.Where(x => x.IsActive).OrderBy(x => x.StreamName).ToListAsync();
                return ApiResponse<List<StreamMaster>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<StreamMaster>>.Fail("Failed to fetch streams", ex.Message);
            }
        }

        public async Task<ApiResponse<StreamMaster>> UpsertAsync(StreamMaster model)
        {
            try
            {
                if (model.StreamId == 0) _db.StreamMasters.Add(model);
                else _db.StreamMasters.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<StreamMaster>.Ok(model, "Saved");
            }
            catch (Exception ex)
            {
                return ApiResponse<StreamMaster>.Fail("Failed to save stream", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> SoftDeleteAsync(short id)
        {
            try
            {
                var row = await _db.StreamMasters.FindAsync(id);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                row.IsActive = false;
                _db.StreamMasters.Update(row);
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
