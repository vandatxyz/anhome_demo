using AnHomes.Application.Dtos;

namespace AnHomes.Application.Interfaces;

public interface IServiceService
{
    Task<PagedResult<ServiceDto>> GetPublishedServicesAsync(int page, int pageSize, CancellationToken ct = default);
    Task<ServiceDto?> GetPublishedServiceBySlugAsync(string slug, CancellationToken ct = default);
    Task<PagedResult<ServiceDto>> GetAllServicesAsync(int page, int pageSize, string? status = null, CancellationToken ct = default);
    Task<ServiceDto> CreateServiceAsync(Dtos.CreateServiceRequest request, CancellationToken ct = default);
    Task<ServiceDto?> UpdateServiceAsync(Guid id, Dtos.UpdateServiceRequest request, CancellationToken ct = default);
    Task<bool> DeleteServiceAsync(Guid id, CancellationToken ct = default);
}
