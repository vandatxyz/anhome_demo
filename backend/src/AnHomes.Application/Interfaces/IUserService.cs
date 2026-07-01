using AnHomes.Application.Dtos;
using AnHomes.Application.Dtos.Auth;
using AnHomes.Application.Dtos;
namespace AnHomes.Application.Interfaces;

public interface IUserService
{
 Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct = default);
 Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
 Task<UserDto?> GetByEmailAsync(string email, CancellationToken ct = default);
 Task<UserDto> CreateAsync(CreateUserRequest request, CancellationToken ct = default);
 Task<UserDto?> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken ct = default);
 Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
 Task<UserDto?> UpdateProfileAsync(Guid id, UpdateProfileRequest request, CancellationToken ct = default);
}

public class CreateUserRequest
{
 public string Email { get; set; } = string.Empty;
 public string Password { get; set; } = string.Empty;
 public string FullName { get; set; } = string.Empty;
 public string Role { get; set; } = "editor";
 public bool IsActive { get; set; } = true;
}

public class UpdateUserRequest : CreateUserRequest
{
 public Guid Id { get; set; }
}

public class UpdateProfileRequest
{
 public string FullName { get; set; } = string.Empty;
 public string? Password { get; set; }
}
