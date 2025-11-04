using System;
using System.ComponentModel.DataAnnotations;

namespace MaaPateshwariUniversity.Domain.Entities;
public class Organization
{
    [Key]
    public long OrganizationId { get; set; }
    public byte OrganizationTypeId { get; set; }
    public string NameEn { get; set; } = "";
    public string? NameHi { get; set; }
    public string? NameLocal { get; set; }
    public long? ParentOrganizationId { get; set; }
    public string? InstituteName { get; set; }
    public string? InstituteCode { get; set; }
    public bool IsAffiliated { get; set; }
    public bool IsAutonomous { get; set; }
    public ushort? CategoryId { get; set; }
    public ushort? SubTypeId { get; set; }
    public string? Specialization { get; set; }
    public ushort? ManagementTypeId { get; set; }
    public ushort? GovernmentCategoryId { get; set; }
    public bool IsMinorityInstitution { get; set; }
    public ushort? MinorityTypeId { get; set; }
    public ushort? YearOfEstablishment { get; set; }
    public byte? LocationTypeId { get; set; }
    public ushort CountryId { get; set; }
    public ushort? StateId { get; set; }
    public uint? DistrictId { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? Pincode { get; set; }
    public decimal? GeoLatitude { get; set; }
    public decimal? GeoLongitude { get; set; }
    public string? OfficialEmail { get; set; }
    public string? TelephoneNumber { get; set; }
    public string? Fax { get; set; }
    public string? ExtensionNumber { get; set; }
    public string? Website { get; set; }
    public string? TwitterLink { get; set; }
    public string? FacebookLink { get; set; }
    public string? LinkedinLink { get; set; }
    public byte? StatusId { get; set; }
    public bool IsVisible { get; set; } = true;
    public byte? OuCategoryId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
