using MaaPateshwariUniversity.Application.DTOs;
using MaaPateshwariUniversity.Application.Models;
using MaaPateshwariUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Interfaces.Academics
{
    public interface IOrganizationProgramService
    {
        Task<ApiResponse<List<ProgramSyllabusDto>>> GetByOrganizationAsync(long organizationId);
        Task<ApiResponse<OrganizationProgram>> UpsertAsync(OrganizationProgram model);
        Task<ApiResponse<bool>> DeleteAsync(long organizationProgramId);
    }
}
