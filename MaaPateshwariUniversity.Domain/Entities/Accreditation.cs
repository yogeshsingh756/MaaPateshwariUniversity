using System;
using System.ComponentModel.DataAnnotations;

namespace MaaPateshwariUniversity.Domain.Entities;
public class AccreditationAgency { [Key] public int AgencyId { get; set; } public string AgencyCode { get; set; } = ""; public string AgencyName { get; set; } = ""; public bool IsActive { get; set; } = true; }
public class OrganizationAccreditation { [Key] public long OrganizationAccreditationId { get; set; } public long OrganizationId { get; set; } public int AgencyId { get; set; } public string? GradeOrStatus { get; set; } public DateTime? ValidFrom { get; set; } public DateTime? ValidTo { get; set; } public string? CertificateUrl { get; set; } public DateTime CreatedAt { get; set; } = DateTime.UtcNow; }
