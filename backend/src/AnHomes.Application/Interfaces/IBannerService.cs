using AnHomes.Application.Dtos;
namespace AnHomes.Application.Interfaces;

public interface IBannerService
{
 Task<IReadOnlyList<BannerDto>> GetAllAsync(CancellationToken ct = default);
 Task<IReadOnlyList<BannerDto>> GetActiveAsync(CancellationToken ct = default);
 Task<BannerDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
 Task<BannerDto> CreateAsync(CreateBannerRequest request, CancellationToken ct = default);
 Task<BannerDto?> UpdateAsync(Guid id, UpdateBannerRequest request, CancellationToken ct = default);
 Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
