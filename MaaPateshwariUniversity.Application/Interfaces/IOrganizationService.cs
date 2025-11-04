using MaaPateshwariUniversity.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace MaaPateshwariUniversity.Application.Interfaces;
public interface IOrganizationService
{
    Task<(IEnumerable<Organization> items,int total)> SearchAsync(string? q, byte? typeId, ushort? stateId, uint? districtId, byte? statusId, bool? visible, int page, int pageSize);
    Task<Organization?> GetAsync(long id);
    Task<Organization> UpsertAsync(Organization org, long? userId=null);
    Task<bool> SoftDeleteAsync(long id, long? userId=null);
}
