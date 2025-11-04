using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface ICourseService
    {
        Task<ApiResponse<List<CourseMaster>>> GetAllAsync(string? q);
        Task<ApiResponse<CourseMaster>> UpsertAsync(CourseMaster model);
        Task<ApiResponse<bool>> DeleteAsync(long id);
    }
}
