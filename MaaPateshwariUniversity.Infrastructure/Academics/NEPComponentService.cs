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
    public class NEPComponentService : INEPComponentService
    {
        private readonly ApplicationDbContext _db;
        public NEPComponentService(ApplicationDbContext db) => _db = db;

        public async Task<ApiResponse<List<NEPComponentType>>> GetAllAsync()
        {
            try
            {
                var list = await _db.NEPComponentTypes.OrderBy(x => x.ComponentName).ToListAsync();
                return ApiResponse<List<NEPComponentType>>.Ok(list);
            }
            catch (Exception ex)
            {
                return ApiResponse<List<NEPComponentType>>.Fail("Failed to fetch components", ex.Message);
            }
        }

        public async Task<ApiResponse<NEPComponentType>> UpsertAsync(NEPComponentType model)
        {
            try
            {
                if (model.ComponentTypeId == 0) _db.NEPComponentTypes.Add(model);
                else _db.NEPComponentTypes.Update(model);

                await _db.SaveChangesAsync();
                return ApiResponse<NEPComponentType>.Ok(model, "Saved");
            }
            catch (Exception ex)
            {
                return ApiResponse<NEPComponentType>.Fail("Failed to save component", ex.Message);
            }
        }

        public async Task<ApiResponse<bool>> DeleteAsync(byte id)
        {
            try
            {
                var row = await _db.NEPComponentTypes.FindAsync(id);
                if (row == null) return ApiResponse<bool>.Fail("Not found");
                _db.NEPComponentTypes.Remove(row);
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
