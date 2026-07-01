using AnHomes.Application.Dtos;
using AnHomes.Application.Dtos.Auth;
using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;
using BCrypt.Net;

namespace AnHomes.Application.Services;

public class UserService : IUserService
{
	private readonly IUnitOfWork _uow;

	public UserService(IUnitOfWork uow) => _uow = uow;

	public async Task<IReadOnlyList<UserDto>> GetAllAsync(CancellationToken ct = default)
	{
		var all = await _uow.Users.GetAllAsync(ct);
		return all.Select(MapToDto).ToList();
	}

	public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
	{
		var u = await _uow.Users.GetByIdAsync(id, ct);
		return u == null ? null : MapToDto(u);
	}

	public async Task<UserDto?> GetByEmailAsync(string email, CancellationToken ct = default)
	{
		var all = await _uow.Users.GetAllAsync(ct);
		var u = all.FirstOrDefault(x => x.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
		return u == null ? null : MapToDto(u);
	}

	public async Task<UserDto> CreateAsync(CreateUserRequest request, CancellationToken ct = default)
	{
		var existing = (await _uow.Users.GetAllAsync(ct))
			.FirstOrDefault(x => x.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase));
		if (existing != null)
			throw new InvalidOperationException("Email already exists");

		var user = new User
		{
			Email = request.Email,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
			FullName = request.FullName,
			Role = request.Role,
			IsActive = request.IsActive
		};
		await _uow.Users.AddAsync(user, ct);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(user);
	}

	public async Task<UserDto?> UpdateAsync(Guid id, UpdateUserRequest request, CancellationToken ct = default)
	{
		var user = await _uow.Users.GetByIdAsync(id, ct);
		if (user == null) return null;

		var emailExists = (await _uow.Users.GetAllAsync(ct))
			.Any(x => x.Email.Equals(request.Email, StringComparison.OrdinalIgnoreCase) && x.Id != id);
		if (emailExists)
			throw new InvalidOperationException("Email already exists");

		user.Email = request.Email;
		user.FullName = request.FullName;
		user.Role = request.Role;
		user.IsActive = request.IsActive;
		if (!string.IsNullOrEmpty(request.Password))
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
		user.UpdatedAt = DateTime.UtcNow;

		_uow.Users.Update(user);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(user);
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
	{
		var user = await _uow.Users.GetByIdAsync(id, ct);
		if (user == null) return false;
		_uow.Users.Delete(user);
		await _uow.SaveChangesAsync(ct);
		return true;
	}

	public async Task<UserDto?> UpdateProfileAsync(Guid id, UpdateProfileRequest request, CancellationToken ct = default)
	{
		var user = await _uow.Users.GetByIdAsync(id, ct);
		if (user == null) return null;
		user.FullName = request.FullName;
		if (!string.IsNullOrEmpty(request.Password))
			user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
		user.UpdatedAt = DateTime.UtcNow;
		_uow.Users.Update(user);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(user);
	}

	private static UserDto MapToDto(User u) => new()
	{
		Id = u.Id,
		Email = u.Email,
		FullName = u.FullName,
		Role = u.Role
	};
}