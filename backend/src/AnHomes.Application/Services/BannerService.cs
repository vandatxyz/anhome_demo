using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

namespace AnHomes.Application.Services;

public class BannerService : IBannerService
{
	private readonly IUnitOfWork _uow;

	public BannerService(IUnitOfWork uow) => _uow = uow;

	public async Task<IReadOnlyList<BannerDto>> GetAllAsync(CancellationToken ct = default)
	{
		var all = await _uow.Banners.GetAllAsync(ct);
		return all.OrderBy(b => b.SortOrder).Select(MapToDto).ToList();
	}

	public async Task<IReadOnlyList<BannerDto>> GetActiveAsync(CancellationToken ct = default)
	{
		var all = await _uow.Banners.GetAllAsync(ct);
		return all.Where(b => b.IsActive).OrderBy(b => b.SortOrder).Select(MapToDto).ToList();
	}

	public async Task<BannerDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
	{
		var b = await _uow.Banners.GetByIdAsync(id, ct);
		return b == null ? null : MapToDto(b);
	}

	public async Task<BannerDto> CreateAsync(CreateBannerRequest request, CancellationToken ct = default)
	{
		var banner = new Banner
		{
			Title = request.Title,
			Subtitle = request.Subtitle,
			ImageUrl = request.ImageUrl,
			ImagePublicId = request.ImagePublicId,
			LinkUrl = request.LinkUrl,
			SortOrder = request.SortOrder,
			IsActive = request.IsActive
		};
		await _uow.Banners.AddAsync(banner, ct);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(banner);
	}

	public async Task<BannerDto?> UpdateAsync(Guid id, UpdateBannerRequest request, CancellationToken ct = default)
	{
		var banner = await _uow.Banners.GetByIdAsync(id, ct);
		if (banner == null) return null;
		banner.Title = request.Title;
		banner.Subtitle = request.Subtitle;
		banner.ImageUrl = request.ImageUrl;
		banner.ImagePublicId = request.ImagePublicId;
		banner.LinkUrl = request.LinkUrl;
		banner.SortOrder = request.SortOrder;
		banner.IsActive = request.IsActive;
		banner.UpdatedAt = DateTime.UtcNow;
		_uow.Banners.Update(banner);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(banner);
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
	{
		var banner = await _uow.Banners.GetByIdAsync(id, ct);
		if (banner == null) return false;
		_uow.Banners.Delete(banner);
		await _uow.SaveChangesAsync(ct);
		return true;
	}

	private static BannerDto MapToDto(Banner b) => new()
	{
		Id = b.Id,
		Title = b.Title,
		Subtitle = b.Subtitle,
		ImageUrl = b.ImageUrl,
		LinkUrl = b.LinkUrl,
		SortOrder = b.SortOrder,
		IsActive = b.IsActive,
		CreatedAt = b.CreatedAt
	};
}