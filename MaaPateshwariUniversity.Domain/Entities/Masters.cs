using System.ComponentModel.DataAnnotations;

namespace MaaPateshwariUniversity.Domain.Entities;
public class OrganizationType { [Key] public byte OrganizationTypeId { get; set; } public string TypeName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class InstitutionCategory { [Key] public ushort CategoryId { get; set; } public string CategoryName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class SubInstitutionType { [Key] public ushort SubTypeId { get; set; } public string SubTypeName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class ManagementType { [Key] public ushort ManagementTypeId { get; set; } public string ManagementTypeName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class GovernmentCategory { [Key] public ushort GovernmentCategoryId { get; set; } public string GovernmentCategoryName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class MinorityType { [Key] public ushort MinorityTypeId { get; set; } public string MinorityTypeName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class LocationType { [Key] public byte LocationTypeId { get; set; } public string LocationTypeName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class State { [Key] public ushort StateId { get; set; } public string StateName { get; set; } = ""; public ushort CountryId { get; set; } public bool IsActive { get; set; } = true; }
public class District { [Key] public uint DistrictId { get; set; } public ushort StateId { get; set; } public string DistrictName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class StatusMaster { [Key] public byte StatusId { get; set; } public string StatusName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class OuCategory { [Key] public byte OuCategoryId { get; set; } public string OuCategoryName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class Country
{
    [Key]
    public ushort CountryId { get; set; }    
    public string CountryName { get; set; } = "";
    public bool IsActive { get; set; } = true;
}
