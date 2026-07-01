using AnHomes.Application.Dtos;

namespace AnHomes.Application.Interfaces;

public interface IPostService
{
    Task<PagedResult<PostDto>> GetPublishedPostsAsync(int page, int pageSize, string? category = null, CancellationToken ct = default);
    Task<PostDto?> GetPublishedPostBySlugAsync(string slug, CancellationToken ct = default);
    Task<PagedResult<PostDto>> GetAllPostsAsync(int page, int pageSize, string? status = null, CancellationToken ct = default);
    Task<PostDto> CreatePostAsync(Dtos.CreatePostRequest request, CancellationToken ct = default);
    Task<PostDto?> UpdatePostAsync(Guid id, Dtos.UpdatePostRequest request, CancellationToken ct = default);
    Task<bool> DeletePostAsync(Guid id, CancellationToken ct = default);
}
