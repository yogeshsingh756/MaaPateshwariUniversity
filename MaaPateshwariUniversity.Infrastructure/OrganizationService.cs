using Microsoft.EntityFrameworkCore;
using MaaPateshwariUniversity.Application.Interfaces;
using MaaPateshwariUniversity.Domain.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace MaaPateshwariUniversity.Infrastructure;
public class OrganizationService : IOrganizationService
{
    private readonly ApplicationDbContext _db;
    public OrganizationService(ApplicationDbContext db){ _db=db; }

    public async Task<(IEnumerable<Organization> items,int total)> SearchAsync(string? q, byte? typeId, ushort? stateId, uint? districtId, byte? statusId, bool? visible, int page, int pageSize)
    {
        var qu = _db.Organizations.AsQueryable();
        if (!string.IsNullOrWhiteSpace(q)) qu = qu.Where(x => x.NameEn.Contains(q));
        if (typeId.HasValue) qu = qu.Where(x => x.OrganizationTypeId == typeId);
        if (stateId.HasValue) qu = qu.Where(x => x.StateId == stateId);
        if (districtId.HasValue) qu = qu.Where(x => x.DistrictId == districtId);
        if (statusId.HasValue) qu = qu.Where(x => x.StatusId == statusId);
        if (visible.HasValue) qu = qu.Where(x => x.IsVisible == visible);
        var total = await qu.CountAsync();
        var items = await qu.OrderBy(x => x.OrganizationId).Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
        return (items,total);
    }

    public async Task<Organization?> GetAsync(long id) => await _db.Organizations.FindAsync(id);

    public async Task<Organization> UpsertAsync(Organization org, long? userId=null)
    {
        var exists = await _db.Organizations.AsNoTracking().AnyAsync(x => x.OrganizationId == org.OrganizationId);
        if (exists) { org.UpdatedBy = userId; _db.Organizations.Update(org); }
        else { org.CreatedBy = userId; await _db.Organizations.AddAsync(org); }
        await _db.SaveChangesAsync();
        return org;
    }

    public async Task<bool> SoftDeleteAsync(long id, long? userId=null)
    {
        var o = await _db.Organizations.FirstOrDefaultAsync(x => x.OrganizationId == id);
        if (o == null) return false;
        o.IsDeleted = true; o.UpdatedBy = userId;
        await _db.SaveChangesAsync();
        return true;
    }
}
