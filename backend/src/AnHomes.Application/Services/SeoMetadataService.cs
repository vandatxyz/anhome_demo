using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

namespace AnHomes.Application.Services;

public class SeoMetadataService : ISeoMetadataService
{
	private readonly IUnitOfWork _uow;

	public SeoMetadataService(IUnitOfWork uow) => _uow = uow;

	public async Task<IReadOnlyList<SeoMetadataDto>> GetAllAsync(CancellationToken ct = default)
	{
		var all = await _uow.SeoMetadatas.GetAllAsync(ct);
		return all.Select(MapToDto).ToList();
	}

	public async Task<SeoMetadataDto?> GetByPageAsync(string page, CancellationToken ct = default)
	{
		var all = await _uow.SeoMetadatas.GetAllAsync(ct);
		var s = all.FirstOrDefault(x => x.Page.Equals(page, StringComparison.OrdinalIgnoreCase));
		return s == null ? null : MapToDto(s);
	}

	public async Task<SeoMetadataDto> CreateAsync(CreateSeoMetadataRequest request, CancellationToken ct = default)
	{
		var meta = new SeoMetadata
		{
			Page = request.Page,
			Title = request.Title,
			Description = request.Description,
			Keywords = request.Keywords,
			OgImage = request.OgImage,
			CanonicalUrl = request.CanonicalUrl,
			SchemaJson = request.SchemaJson
		};
		await _uow.SeoMetadatas.AddAsync(meta, ct);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(meta);
	}

	public async Task<SeoMetadataDto?> UpdateAsync(Guid id, UpdateSeoMetadataRequest request, CancellationToken ct = default)
	{
		var meta = await _uow.SeoMetadatas.GetByIdAsync(id, ct);
		if (meta == null) return null;
		meta.Page = request.Page;
		meta.Title = request.Title;
		meta.Description = request.Description;
		meta.Keywords = request.Keywords;
		meta.OgImage = request.OgImage;
		meta.CanonicalUrl = request.CanonicalUrl;
		meta.SchemaJson = request.SchemaJson;
		meta.UpdatedAt = DateTime.UtcNow;
		_uow.SeoMetadatas.Update(meta);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(meta);
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
	{
		var meta = await _uow.SeoMetadatas.GetByIdAsync(id, ct);
		if (meta == null) return false;
		_uow.SeoMetadatas.Delete(meta);
		await _uow.SaveChangesAsync(ct);
		return true;
	}

	private static SeoMetadataDto MapToDto(SeoMetadata s) => new()
	{
		Id = s.Id,
		Page = s.Page,
		Title = s.Title,
		Description = s.Description,
		Keywords = s.Keywords,
		OgImage = s.OgImage,
		CanonicalUrl = s.CanonicalUrl,
		SchemaJson = s.SchemaJson,
		UpdatedAt = s.UpdatedAt
	};
}