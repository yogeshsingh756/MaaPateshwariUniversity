using MaaPateshwariUniversity.Application.DTOs;
using System.Threading.Tasks;
namespace MaaPateshwariUniversity.Application.Interfaces;
public interface IAuthService { Task<LoginResponse> LoginAsync(LoginRequest request); Task<string?> RefreshAsync(RefreshRequest request); }
