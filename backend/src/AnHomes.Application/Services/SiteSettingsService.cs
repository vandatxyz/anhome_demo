using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

namespace AnHomes.Application.Services;

public class SiteSettingsService : ISiteSettingsService
{
	private readonly IUnitOfWork _uow;

	public SiteSettingsService(IUnitOfWork uow) => _uow = uow;

	public async Task<IReadOnlyList<SiteSettingDto>> GetAllAsync(CancellationToken ct = default)
	{
		var all = await _uow.SiteSettings.GetAllAsync(ct);
		return all.Select(MapToDto).ToList();
	}

	public async Task<IReadOnlyList<SiteSettingDto>> GetByGroupAsync(string group, CancellationToken ct = default)
	{
		var all = await _uow.SiteSettings.GetAllAsync(ct);
		return all.Where(s => s.Group == group).Select(MapToDto).ToList();
	}

	public async Task<SiteSettingDto?> GetByKeyAsync(string key, string group, CancellationToken ct = default)
	{
		var all = await _uow.SiteSettings.GetAllAsync(ct);
		var s = all.FirstOrDefault(x => x.Key == key && x.Group == group);
		return s == null ? null : MapToDto(s);
	}

	public async Task<SiteSettingDto> CreateAsync(CreateSiteSettingRequest request, CancellationToken ct = default)
	{
		var setting = new SiteSetting
		{
			Key = request.Key,
			Value = request.Value,
			Group = request.Group,
			Description = request.Description
		};
		await _uow.SiteSettings.AddAsync(setting, ct);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(setting);
	}

	public async Task<SiteSettingDto?> UpdateAsync(Guid id, UpdateSiteSettingRequest request, CancellationToken ct = default)
	{
		var setting = await _uow.SiteSettings.GetByIdAsync(id, ct);
		if (setting == null) return null;
		setting.Key = request.Key;
		setting.Value = request.Value;
		setting.Group = request.Group;
		setting.Description = request.Description;
		setting.UpdatedAt = DateTime.UtcNow;
		_uow.SiteSettings.Update(setting);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(setting);
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
	{
		var setting = await _uow.SiteSettings.GetByIdAsync(id, ct);
		if (setting == null) return false;
		_uow.SiteSettings.Delete(setting);
		await _uow.SaveChangesAsync(ct);
		return true;
	}

	public async Task<Dictionary<string, string>> GetAllAsDictionaryAsync(CancellationToken ct = default)
	{
		var all = await _uow.SiteSettings.GetAllAsync(ct);
		return all.ToDictionary(s => s.Key, s => s.Value);
	}

	private static SiteSettingDto MapToDto(SiteSetting s) => new()
	{
		Id = s.Id,
		Key = s.Key,
		Value = s.Value,
		Group = s.Group,
		Description = s.Description,
		UpdatedAt = s.UpdatedAt
	};
}