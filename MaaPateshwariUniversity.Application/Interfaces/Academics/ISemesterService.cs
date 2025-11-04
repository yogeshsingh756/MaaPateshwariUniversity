using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface ISemesterService
    {
        Task<ApiResponse<List<SemesterMaster>>> GetAllAsync();
        Task<ApiResponse<SemesterMaster>> UpsertAsync(SemesterMaster model);
        Task<ApiResponse<bool>> DeleteAsync(byte id);
    }
}
