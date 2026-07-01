using AnHomes.Application.Dtos;
namespace AnHomes.Application.Interfaces;

public interface ISiteSettingsService
{
 Task<IReadOnlyList<SiteSettingDto>> GetAllAsync(CancellationToken ct = default);
 Task<IReadOnlyList<SiteSettingDto>> GetByGroupAsync(string group, CancellationToken ct = default);
 Task<SiteSettingDto?> GetByKeyAsync(string key, string group, CancellationToken ct = default);
 Task<SiteSettingDto> CreateAsync(CreateSiteSettingRequest request, CancellationToken ct = default);
 Task<SiteSettingDto?> UpdateAsync(Guid id, UpdateSiteSettingRequest request, CancellationToken ct = default);
 Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
 Task<Dictionary<string, string>> GetAllAsDictionaryAsync(CancellationToken ct = default);
}
