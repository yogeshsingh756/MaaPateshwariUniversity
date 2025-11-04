using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface IProgramCourseService
    {
        Task<ApiResponse<List<ProgramCourseMapping>>> GetByProgramAsync(long programId);
        Task<ApiResponse<ProgramCourseMapping>> UpsertAsync(ProgramCourseMapping model);
        Task<ApiResponse<bool>> DeleteAsync(long programCourseId);
    }
}
