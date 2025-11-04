using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface INEPComponentService
    {
        Task<ApiResponse<List<NEPComponentType>>> GetAllAsync();
        Task<ApiResponse<NEPComponentType>> UpsertAsync(NEPComponentType model);
        Task<ApiResponse<bool>> DeleteAsync(byte id);
    }
}
