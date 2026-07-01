using AnHomes.Application.Dtos;

namespace AnHomes.Application.Interfaces;

public interface IMediaService
{
    Task<IReadOnlyList<MediaDto>> GetAllAsync(string? folder = null, CancellationToken ct = default);
    Task<MediaDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<MediaDto> UploadAsync(Stream fileStream, string fileName, string contentType, string? folder = null, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> DeleteByPublicIdAsync(string publicId, CancellationToken ct = default);
}
