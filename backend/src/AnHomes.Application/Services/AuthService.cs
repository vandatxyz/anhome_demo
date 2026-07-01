using AnHomes.Application.Dtos;
using AnHomes.Application.Dtos.Auth;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;
using BCrypt.Net;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnHomes.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _uow;
    private readonly string _jwtSecret;
    private readonly string _jwtIssuer;
    private readonly string _jwtAudience;
    private readonly int _accessTokenMinutes;
    private readonly int _refreshTokenDays;

    public AuthService(IUnitOfWork uow, IOptions<JwtSettings> jwtOptions)
    {
        _uow = uow;
        var j = jwtOptions.Value;
        _jwtSecret = j.Secret;
        _jwtIssuer = j.Issuer;
        _jwtAudience = j.Audience;
        _accessTokenMinutes = j.AccessTokenMinutes;
        _refreshTokenDays = j.RefreshTokenDays;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request, string? ipAddress = null, CancellationToken ct = default)
    {
        var users = await _uow.Users.FindAsync(u => u.Email == request.Email, ct);
        var user = users.FirstOrDefault();
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return null;
        if (!user.IsActive) return null;

        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();

        user.RefreshTokens.Add(new RefreshToken { Token = refreshToken, ExpiresAt = DateTime.UtcNow.AddDays(_refreshTokenDays) });
        user.LastLoginAt = DateTime.UtcNow;
        _uow.Users.Update(user);

        _uow.AuditLogs.AddAsync(new AuditLog { UserId = user.Id, Action = "Login", EntityType = "User", EntityId = user.Id.ToString(), IpAddress = ipAddress }, ct);
        await _uow.SaveChangesAsync(ct);

        return new LoginResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            User = new UserDto { Id = user.Id, Email = user.Email, FullName = user.FullName, Role = user.Role }
        };
    }

    public async Task<RefreshTokenResponse?> RefreshTokenAsync(string refreshToken, CancellationToken ct = default)
    {
        var tokens = await _uow.RefreshTokens.FindAsync(t => t.Token == refreshToken, ct);
        var token = tokens.FirstOrDefault();
        if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow) return null;

        var user = await _uow.Users.GetByIdAsync(token.UserId, ct);
        if (user == null || !user.IsActive) return null;

        token.IsRevoked = true;
        var newAccessToken = GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshTokens.Add(new RefreshToken { Token = newRefreshToken, ExpiresAt = DateTime.UtcNow.AddDays(_refreshTokenDays) });
        _uow.Users.Update(user);
        await _uow.SaveChangesAsync(ct);

        return new RefreshTokenResponse { AccessToken = newAccessToken };
    }

    public async Task<bool> LogoutAsync(string refreshToken, CancellationToken ct = default)
    {
        var tokens = await _uow.RefreshTokens.FindAsync(t => t.Token == refreshToken, ct);
        var token = tokens.FirstOrDefault();
        if (token == null) return false;
        token.IsRevoked = true;
        _uow.RefreshTokens.Update(token);
        _uow.AuditLogs.AddAsync(new AuditLog { UserId = token.UserId, Action = "Logout", EntityType = "User", EntityId = token.UserId.ToString() }, ct);
        await _uow.SaveChangesAsync(ct);
        return true;
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _uow.Users.GetByIdAsync(userId, ct);
        if (user == null) return null;
        return new UserDto { Id = user.Id, Email = user.Email, FullName = user.FullName, Role = user.Role };
    }

    private string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Role, user.Role)
        };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_jwtIssuer, _jwtAudience, claims, expires: DateTime.UtcNow.AddMinutes(_accessTokenMinutes), signingCredentials: creds);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int AccessTokenMinutes { get; set; } = 15;
    public int RefreshTokenDays { get; set; } = 7;
}
