using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

namespace AnHomes.Application.Services;

public class PostService : IPostService
{
    private readonly IUnitOfWork _uow;

    public PostService(IUnitOfWork uow) => _uow = uow;

    public async Task<PagedResult<PostDto>> GetPublishedPostsAsync(int page, int pageSize, string? category = null, CancellationToken ct = default)
    {
        var allPosts = await _uow.Posts.GetAllAsync(ct);
        var allCategories = await _uow.Categories.GetAllAsync(ct);
        var catDict = allCategories.ToDictionary(c => c.Id);

        var query = allPosts.Where(p => p.Status == "published").AsQueryable();
        if (!string.IsNullOrEmpty(category))
        {
            var catSlug = category;
            var catIds = catDict.Where(kv => kv.Value.Slug == catSlug).Select(kv => kv.Key).ToHashSet();
            query = query.Where(p => catIds.Contains(p.CategoryId));
        }
        var total = query.Count();
        var items = query.OrderByDescending(p => p.PublishedAt).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<PostDto> { Data = items.Select(p => MapToDto(p, catDict)).ToList(), Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<PostDto?> GetPublishedPostBySlugAsync(string slug, CancellationToken ct = default)
    {
        var posts = await _uow.Posts.FindAsync(p => p.Slug == slug && p.Status == "published", ct);
        var post = posts.FirstOrDefault();
        if (post == null) return null;
        var category = await _uow.Categories.GetByIdAsync(post.CategoryId, ct);
        var catDict = category != null ? new Dictionary<Guid, Category> { [post.CategoryId] = category } : new Dictionary<Guid, Category>();
        return MapToDto(post, catDict);
    }

    public async Task<PagedResult<PostDto>> GetAllPostsAsync(int page, int pageSize, string? status = null, CancellationToken ct = default)
    {
        var allPosts = await _uow.Posts.GetAllAsync(ct);
        var allCategories = await _uow.Categories.GetAllAsync(ct);
        var catDict = allCategories.ToDictionary(c => c.Id);

        var query = allPosts.AsQueryable();
        if (!string.IsNullOrEmpty(status)) query = query.Where(p => p.Status == status);
        var total = query.Count();
        var items = query.OrderByDescending(p => p.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<PostDto> { Data = items.Select(p => MapToDto(p, catDict)).ToList(), Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<PostDto> CreatePostAsync(Dtos.CreatePostRequest request, CancellationToken ct = default)
    {
        var post = new Post
        {
            Title = request.Title,
            Slug = GenerateSlug(request.Title),
            ShortDescription = request.ShortDescription,
            Content = request.Content,
            CategoryId = request.CategoryId,
            SeoTitle = request.SeoTitle,
            SeoDescription = request.SeoDescription,
            Status = "draft"
        };
        await _uow.Posts.AddAsync(post, ct);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(post);
    }

    public async Task<PostDto?> UpdatePostAsync(Guid id, Dtos.UpdatePostRequest request, CancellationToken ct = default)
    {
        var post = await _uow.Posts.GetByIdAsync(id, ct);
        if (post == null) return null;
        post.Title = request.Title;
        post.ShortDescription = request.ShortDescription;
        post.Content = request.Content;
        post.CategoryId = request.CategoryId;
        post.SeoTitle = request.SeoTitle;
        post.SeoDescription = request.SeoDescription;
        post.Status = request.Status;
        post.UpdatedAt = DateTime.UtcNow;
        if (request.Status == "published" && post.PublishedAt == null)
            post.PublishedAt = DateTime.UtcNow;
        _uow.Posts.Update(post);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(post);
    }

    public async Task<bool> DeletePostAsync(Guid id, CancellationToken ct = default)
    {
        var post = await _uow.Posts.GetByIdAsync(id, ct);
        if (post == null) return false;
        _uow.Posts.Delete(post);
        await _uow.SaveChangesAsync(ct);
        return true;
    }

    private static PostDto MapToDto(Post p, Dictionary<Guid, Category>? catDict = null)
    {
        catDict ??= new Dictionary<Guid, Category>();
        var catName = catDict.TryGetValue(p.CategoryId, out var cat) ? cat.Name : null;
        return new PostDto
        {
            Id = p.Id, Title = p.Title, Slug = p.Slug, ShortDescription = p.ShortDescription,
            Content = p.Content, CoverImage = p.CoverImage, CategoryId = p.CategoryId.ToString(),
            CategoryName = catName, SeoTitle = p.SeoTitle, SeoDescription = p.SeoDescription,
            Status = p.Status, CreatedAt = p.CreatedAt, PublishedAt = p.PublishedAt
        };
    }

    private static string GenerateSlug(string title)
    {
        var slug = title.ToLowerInvariant().Replace("đ", "d").Replace("ă", "a").Replace("â", "a").Replace("ê", "e").Replace("ô", "o").Replace("ư", "u").Replace(" ", "-");
        return new string(slug.Where(c => char.IsLetterOrDigit(c) || c == '-').ToArray());
    }
}
