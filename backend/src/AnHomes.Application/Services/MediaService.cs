using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;
using AnHomes.Infrastructure.Services;

namespace AnHomes.Application.Services;

public class MediaService : IMediaService
{
	private readonly IUnitOfWork _uow;
	private readonly ICloudinaryService _cloudinary;

	public MediaService(IUnitOfWork uow, ICloudinaryService cloudinary)
	{
		_uow = uow;
		_cloudinary = cloudinary;
	}

	public async Task<IReadOnlyList<MediaDto>> GetAllAsync(string? folder = null, CancellationToken ct = default)
	{
		var all = await _uow.MediaFiles.GetAllAsync(ct);
		var query = all.AsEnumerable();
		if (!string.IsNullOrEmpty(folder))
			query = query.Where(m => m.Folder == folder);
		return query.OrderByDescending(m => m.CreatedAt).Select(MapToDto).ToList();
	}

	public async Task<MediaDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
	{
		var m = await _uow.MediaFiles.GetByIdAsync(id, ct);
		return m == null ? null : MapToDto(m);
	}

	public async Task<MediaDto> UploadAsync(Stream fileStream, string fileName, string contentType, string? folder = null, CancellationToken ct = default)
	{
		var (url, publicId) = await _cloudinary.UploadImageAsync(fileStream, fileName, folder ?? "general", ct);
		var media = new MediaFile
		{
			Url = url,
			PublicId = publicId,
			FileName = fileName,
			ContentType = contentType,
			FileSize = fileStream.Length,
			Folder = folder
		};
		await _uow.MediaFiles.AddAsync(media, ct);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(media);
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
	{
		var media = await _uow.MediaFiles.GetByIdAsync(id, ct);
		if (media == null) return false;
		try
		{
			await _cloudinary.DeleteImageAsync(media.PublicId, ct);
		}
		catch { /* ignore */ }
		_uow.MediaFiles.Delete(media);
		await _uow.SaveChangesAsync(ct);
		return true;
	}

	public async Task<bool> DeleteByPublicIdAsync(string publicId, CancellationToken ct = default)
	{
		var all = await _uow.MediaFiles.GetAllAsync(ct);
		var media = all.FirstOrDefault(m => m.PublicId == publicId);
		if (media == null) return false;
		_uow.MediaFiles.Delete(media);
		await _uow.SaveChangesAsync(ct);
		return true;
	}

	private static MediaDto MapToDto(MediaFile m) => new()
	{
		Id = m.Id,
		Url = m.Url,
		PublicId = m.PublicId,
		AltText = m.AltText,
		FileName = m.FileName,
		FileSize = m.FileSize,
		Folder = m.Folder,
		CreatedAt = m.CreatedAt
	};
}