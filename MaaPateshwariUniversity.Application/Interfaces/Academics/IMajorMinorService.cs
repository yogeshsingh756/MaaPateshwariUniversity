using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface IMajorMinorService
    {
        Task<ApiResponse<List<MajorMinorMapping>>> GetByMajorAsync(long majorProgramId);
        Task<ApiResponse<MajorMinorMapping>> UpsertAsync(MajorMinorMapping model);
        Task<ApiResponse<bool>> DeleteAsync(long majorMinorId);
    }
}
