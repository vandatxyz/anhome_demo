using AnHomes.Application.Dtos;
namespace AnHomes.Application.Interfaces;

public interface ISeoMetadataService
{
 Task<IReadOnlyList<SeoMetadataDto>> GetAllAsync(CancellationToken ct = default);
 Task<SeoMetadataDto?> GetByPageAsync(string page, CancellationToken ct = default);
 Task<SeoMetadataDto> CreateAsync(CreateSeoMetadataRequest request, CancellationToken ct = default);
 Task<SeoMetadataDto?> UpdateAsync(Guid id, UpdateSeoMetadataRequest request, CancellationToken ct = default);
 Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
