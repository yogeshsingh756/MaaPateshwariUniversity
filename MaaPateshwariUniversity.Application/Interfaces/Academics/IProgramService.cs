using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface IProgramService
    {
        Task<ApiResponse<List<ProgramMaster>>> GetAllAsync(short? disciplineId);
        Task<ApiResponse<ProgramMaster>> UpsertAsync(ProgramMaster model);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
