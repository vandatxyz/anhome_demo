using AnHomes.Application.Dtos;

namespace AnHomes.Application.Interfaces;

public interface IProjectService
{
    Task<PagedResult<ProjectDto>> GetPublishedProjectsAsync(int page, int pageSize, string? category = null, string? style = null, CancellationToken ct = default);
    Task<ProjectDto?> GetPublishedProjectBySlugAsync(string slug, CancellationToken ct = default);
    Task<ProjectDto?> GetProjectByIdAsync(Guid id, CancellationToken ct = default);
    Task<PagedResult<ProjectDto>> GetAllProjectsAsync(int page, int pageSize, string? status = null, CancellationToken ct = default);
    Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request, CancellationToken ct = default);
    Task<ProjectDto?> UpdateProjectAsync(Guid id, UpdateProjectRequest request, CancellationToken ct = default);
    Task<bool> DeleteProjectAsync(Guid id, CancellationToken ct = default);
}
