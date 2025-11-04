using MaaPateshwariUniversity.Domain.Entities;
using MaaPateshwariUniversity.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace MaaPateshwariUniversity.API.Controllers;

[ApiController]
[Route("api/masters")]
[Authorize(Roles="SuperAdmin,UniversityAdmin")]
public class MastersController : ControllerBase
{
    private readonly ApplicationDbContext _db;
    public MastersController(ApplicationDbContext db){ _db=_db = db; }
    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    => Ok(await _db.Countries.OrderBy(x => x.CountryName).ToListAsync());

    [HttpPost("countries")]
    public async Task<IActionResult> UpsertCountry(Country c)
    {
        if (await _db.Countries.AnyAsync(x => x.CountryId == c.CountryId))
            _db.Countries.Update(c);
        else
            _db.Countries.Add(c);

        await _db.SaveChangesAsync();
        return Ok(c);
    }

    [HttpDelete("countries/{id}")]
    public async Task<IActionResult> DeleteCountry(ushort id)
    {
        var c = await _db.Countries.FindAsync(id);
        if (c == null) return NotFound();
        _db.Countries.Remove(c);
        await _db.SaveChangesAsync();
        return Ok();
    }
    [HttpGet("states")] public async Task<IActionResult> GetStates() => Ok(await _db.States.OrderBy(x=>x.StateName).ToListAsync());
    [HttpPost("states")] public async Task<IActionResult> UpsertState(State s){ if (await _db.States.AnyAsync(x=>x.StateId==s.StateId)) _db.States.Update(s); else _db.States.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("states/{id}")] public async Task<IActionResult> DelState(ushort id){ var s=await _db.States.FindAsync(id); if(s==null) return NotFound(); _db.States.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("districts")] public async Task<IActionResult> GetDistricts() => Ok(await _db.Districts.OrderBy(x=>x.StateId).ThenBy(x=>x.DistrictName).ToListAsync());
    [HttpPost("districts")] public async Task<IActionResult> UpsertDistrict(District s){ if (await _db.Districts.AnyAsync(x=>x.DistrictId==s.DistrictId)) _db.Districts.Update(s); else _db.Districts.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("districts/{id}")] public async Task<IActionResult> DelDistrict(uint id){ var s=await _db.Districts.FindAsync(id); if(s==null) return NotFound(); _db.Districts.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("organization-types")] public async Task<IActionResult> GetOrgTypes() => Ok(await _db.OrganizationTypes.OrderBy(x=>x.TypeName).ToListAsync());
    [HttpPost("organization-types")] public async Task<IActionResult> UpsertOrgType(OrganizationType s){ if (await _db.OrganizationTypes.AnyAsync(x=>x.OrganizationTypeId==s.OrganizationTypeId)) _db.OrganizationTypes.Update(s); else _db.OrganizationTypes.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("organization-types/{id}")] public async Task<IActionResult> DelOrgType(byte id){ var s=await _db.OrganizationTypes.FindAsync(id); if(s==null) return NotFound(); _db.OrganizationTypes.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("categories")] public async Task<IActionResult> GetCategories() => Ok(await _db.InstitutionCategories.OrderBy(x=>x.CategoryName).ToListAsync());
    [HttpPost("categories")] public async Task<IActionResult> UpsertCategory(InstitutionCategory s){ if ( await _db.InstitutionCategories.AnyAsync(x=>x.CategoryId==s.CategoryId)) _db.InstitutionCategories.Update(s); else _db.InstitutionCategories.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("categories/{id}")] public async Task<IActionResult> DelCategory(ushort id){ var s=await _db.InstitutionCategories.FindAsync(id); if(s==null) return NotFound(); _db.InstitutionCategories.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("subtypes")] public async Task<IActionResult> GetSubTypes() => Ok(await _db.SubInstitutionTypes.OrderBy(x=>x.SubTypeName).ToListAsync());
    [HttpPost("subtypes")] public async Task<IActionResult> UpsertSubType(SubInstitutionType s){ if ( await _db.SubInstitutionTypes.AnyAsync(x=>x.SubTypeId==s.SubTypeId)) _db.SubInstitutionTypes.Update(s); else _db.SubInstitutionTypes.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("subtypes/{id}")] public async Task<IActionResult> DelSubType(ushort id){ var s=await _db.SubInstitutionTypes.FindAsync(id); if(s==null) return NotFound(); _db.SubInstitutionTypes.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("management-types")] public async Task<IActionResult> GetMgmtTypes() => Ok(await _db.ManagementTypes.OrderBy(x=>x.ManagementTypeName).ToListAsync());
    [HttpPost("management-types")] public async Task<IActionResult> UpsertMgmtType(ManagementType s){ if ( await _db.ManagementTypes.AnyAsync(x=>x.ManagementTypeId==s.ManagementTypeId)) _db.ManagementTypes.Update(s); else _db.ManagementTypes.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("management-types/{id}")] public async Task<IActionResult> DelMgmtType(ushort id){ var s=await _db.ManagementTypes.FindAsync(id); if(s==null) return NotFound(); _db.ManagementTypes.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("government-categories")] public async Task<IActionResult> GetGovCats() => Ok(await _db.GovernmentCategories.OrderBy(x=>x.GovernmentCategoryName).ToListAsync());
    [HttpPost("government-categories")] public async Task<IActionResult> UpsertGovCat(GovernmentCategory s){ if ( await _db.GovernmentCategories.AnyAsync(x=>x.GovernmentCategoryId==s.GovernmentCategoryId)) _db.GovernmentCategories.Update(s); else _db.GovernmentCategories.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("government-categories/{id}")] public async Task<IActionResult> DelGovCat(ushort id){ var s=await _db.GovernmentCategories.FindAsync(id); if(s==null) return NotFound(); _db.GovernmentCategories.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("minority-types")] public async Task<IActionResult> GetMinorityTypes() => Ok(await _db.MinorityTypes.OrderBy(x=>x.MinorityTypeName).ToListAsync());
    [HttpPost("minority-types")] public async Task<IActionResult> UpsertMinorityType(MinorityType s){ if ( await _db.MinorityTypes.AnyAsync(x=>x.MinorityTypeId==s.MinorityTypeId)) _db.MinorityTypes.Update(s); else _db.MinorityTypes.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("minority-types/{id}")] public async Task<IActionResult> DelMinorityType(ushort id){ var s=await _db.MinorityTypes.FindAsync(id); if(s==null) return NotFound(); _db.MinorityTypes.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("location-types")] public async Task<IActionResult> GetLocTypes() => Ok(await _db.LocationTypes.OrderBy(x=>x.LocationTypeName).ToListAsync());
    [HttpPost("location-types")] public async Task<IActionResult> UpsertLocType(LocationType s){ if ( await _db.LocationTypes.AnyAsync(x=>x.LocationTypeId==s.LocationTypeId)) _db.LocationTypes.Update(s); else _db.LocationTypes.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("location-types/{id}")] public async Task<IActionResult> DelLocType(byte id){ var s=await _db.LocationTypes.FindAsync(id); if(s==null) return NotFound(); _db.LocationTypes.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("statuses")] public async Task<IActionResult> GetStatuses() => Ok(await _db.StatusMasters.OrderBy(x=>x.StatusName).ToListAsync());
    [HttpPost("statuses")] public async Task<IActionResult> UpsertStatus(StatusMaster s){ if ( await _db.StatusMasters.AnyAsync(x=>x.StatusId==s.StatusId)) _db.StatusMasters.Update(s); else _db.StatusMasters.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("statuses/{id}")] public async Task<IActionResult> DelStatus(byte id){ var s=await _db.StatusMasters.FindAsync(id); if(s==null) return NotFound(); _db.StatusMasters.Remove(s); await _db.SaveChangesAsync(); return Ok(); }

    [HttpGet("ou-categories")] public async Task<IActionResult> GetOuCats() => Ok(await _db.OuCategories.OrderBy(x=>x.OuCategoryName).ToListAsync());
    [HttpPost("ou-categories")] public async Task<IActionResult> UpsertOuCat(OuCategory s){ if ( await _db.OuCategories.AnyAsync(x=>x.OuCategoryId==s.OuCategoryId)) _db.OuCategories.Update(s); else _db.OuCategories.Add(s); await _db.SaveChangesAsync(); return Ok(s); }
    [HttpDelete("ou-categories/{id}")] public async Task<IActionResult> DelOuCat(byte id){ var s=await _db.OuCategories.FindAsync(id); if(s==null) return NotFound(); _db.OuCategories.Remove(s); await _db.SaveChangesAsync(); return Ok(); }
}
