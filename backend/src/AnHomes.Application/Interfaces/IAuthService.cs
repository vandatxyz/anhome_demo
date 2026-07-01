using AnHomes.Application.Dtos;
using AnHomes.Application.Dtos.Auth;

namespace AnHomes.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request, string? ipAddress = null, CancellationToken ct = default);
    Task<RefreshTokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken ct = default);
    Task<bool> LogoutAsync(string refreshToken, CancellationToken ct = default);
    Task<UserDto?> GetUserByIdAsync(Guid userId, CancellationToken ct = default);
}
