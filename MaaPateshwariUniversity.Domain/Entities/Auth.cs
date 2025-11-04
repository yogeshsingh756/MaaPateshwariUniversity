using System;
using System.ComponentModel.DataAnnotations;

namespace MaaPateshwariUniversity.Domain.Entities;
public class Role { [Key] public int RoleId { get; set; } public string Name { get; set; } = ""; }
public class User { [Key] public long UserId { get; set; } public string Email { get; set; } = ""; public string PasswordHash { get; set; } = ""; public int RoleId { get; set; } public ulong? OrganizationId { get; set; } public bool IsActive { get; set; } = true; }
public class RefreshToken { [Key] public long RefreshTokenId { get; set; } public long UserId { get; set; } public string Token { get; set; } = ""; public DateTime ExpiresAt { get; set; } public bool Revoked { get; set; } = false; }
