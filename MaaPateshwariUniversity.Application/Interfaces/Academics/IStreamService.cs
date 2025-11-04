using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface IStreamService
    {
        Task<ApiResponse<List<StreamMaster>>> GetAllAsync();
        Task<ApiResponse<StreamMaster>> UpsertAsync(StreamMaster model);
        Task<ApiResponse<bool>> SoftDeleteAsync(short id);
    }
}
