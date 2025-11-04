namespace MaaPateshwariUniversity.Application.DTOs;
public record LoginRequest(string Username, string Password);
public record LoginResponse(bool Success, string? Token, string? RefreshToken, string? Role, int ExpiresIn);
public record RefreshRequest(string RefreshToken);
