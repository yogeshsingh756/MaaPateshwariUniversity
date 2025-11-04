using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface IDisciplineService
    {
        Task<ApiResponse<List<DisciplineMaster>>> GetAllAsync(short? streamId);
        Task<ApiResponse<DisciplineMaster>> UpsertAsync(DisciplineMaster model);
        Task<ApiResponse<bool>> SoftDeleteAsync(short id);
    }
}
