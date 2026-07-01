using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

namespace AnHomes.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IUnitOfWork _uow;

    public ProjectService(IUnitOfWork uow) => _uow = uow;

    public async Task<PagedResult<ProjectDto>> GetPublishedProjectsAsync(int page, int pageSize, string? category = null, string? style = null, CancellationToken ct = default)
    {
        var allProjects = await _uow.Projects.GetAllAsync(ct);
        var allCategories = await _uow.Categories.GetAllAsync(ct);
        var catDict = allCategories.ToDictionary(c => c.Id);

        var query = allProjects.Where(p => p.Status == "published").AsQueryable();
        if (!string.IsNullOrEmpty(category))
        {
            var catSlug = category;
            var catIds = catDict.Where(kv => kv.Value.Slug == catSlug).Select(kv => kv.Key).ToHashSet();
            query = query.Where(p => catIds.Contains(p.CategoryId));
        }
        if (!string.IsNullOrEmpty(style))
            query = query.Where(p => p.Style.Contains(style));

        var total = query.Count();
        var items = query.OrderByDescending(p => p.PublishedAt).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<ProjectDto> { Data = items.Select(p => MapToDto(p, catDict)).ToList(), Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<ProjectDto?> GetPublishedProjectBySlugAsync(string slug, CancellationToken ct = default)
    {
        var projects = await _uow.Projects.FindAsync(p => p.Slug == slug && p.Status == "published", ct);
        var project = projects.FirstOrDefault();
        if (project == null) return null;
        var category = await _uow.Categories.GetByIdAsync(project.CategoryId, ct);
        var catDict = category != null ? new Dictionary<Guid, Category> { [project.CategoryId] = category } : new Dictionary<Guid, Category>();
        return MapToDto(project, catDict);
    }

    public async Task<ProjectDto?> GetProjectByIdAsync(Guid id, CancellationToken ct = default)
    {
        var project = await _uow.Projects.GetByIdAsync(id, ct);
        if (project == null) return null;
        var category = await _uow.Categories.GetByIdAsync(project.CategoryId, ct);
        var catDict = category != null ? new Dictionary<Guid, Category> { [project.CategoryId] = category } : new Dictionary<Guid, Category>();
        return MapToDto(project, catDict);
    }

    public async Task<PagedResult<ProjectDto>> GetAllProjectsAsync(int page, int pageSize, string? status = null, CancellationToken ct = default)
    {
        var allProjects = await _uow.Projects.GetAllAsync(ct);
        var allCategories = await _uow.Categories.GetAllAsync(ct);
        var catDict = allCategories.ToDictionary(c => c.Id);

        var query = allProjects.AsQueryable();
        if (!string.IsNullOrEmpty(status)) query = query.Where(p => p.Status == status);
        var total = query.Count();
        var items = query.OrderByDescending(p => p.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<ProjectDto> { Data = items.Select(p => MapToDto(p, catDict)).ToList(), Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<ProjectDto> CreateProjectAsync(CreateProjectRequest request, CancellationToken ct = default)
    {
        var project = new Project
        {
            Title = request.Title,
            Slug = GenerateSlug(request.Title),
            ShortDescription = request.ShortDescription,
            Content = request.Content,
            CategoryId = request.CategoryId,
            Style = request.Style,
            Area = request.Area,
            Location = request.Location,
            Year = request.Year,
            IsFeatured = request.IsFeatured,
            SeoTitle = request.SeoTitle,
            SeoDescription = request.SeoDescription,
            Status = "draft"
        };
        await _uow.Projects.AddAsync(project, ct);
        await _uow.SaveChangesAsync(ct);
        return await GetProjectByIdAsync(project.Id, ct) ?? MapToDto(project, new Dictionary<Guid, Category>());
    }

    public async Task<ProjectDto?> UpdateProjectAsync(Guid id, UpdateProjectRequest request, CancellationToken ct = default)
    {
        var project = await _uow.Projects.GetByIdAsync(id, ct);
        if (project == null) return null;
        project.Title = request.Title;
        project.ShortDescription = request.ShortDescription;
        project.Content = request.Content;
        project.CategoryId = request.CategoryId;
        project.Style = request.Style;
        project.Area = request.Area;
        project.Location = request.Location;
        project.Year = request.Year;
        project.IsFeatured = request.IsFeatured;
        project.SeoTitle = request.SeoTitle;
        project.SeoDescription = request.SeoDescription;
        project.Status = request.Status;
        project.UpdatedAt = DateTime.UtcNow;
        if (request.Status == "published" && project.PublishedAt == null)
            project.PublishedAt = DateTime.UtcNow;
        _uow.Projects.Update(project);
        await _uow.SaveChangesAsync(ct);
        return await GetProjectByIdAsync(project.Id, ct);
    }

    public async Task<bool> DeleteProjectAsync(Guid id, CancellationToken ct = default)
    {
        var project = await _uow.Projects.GetByIdAsync(id, ct);
        if (project == null) return false;
        var images = await _uow.ProjectImages.FindAsync(i => i.ProjectId == id, ct);
        _uow.ProjectImages.DeleteRange(images);
        _uow.Projects.Delete(project);
        await _uow.SaveChangesAsync(ct);
        return true;
    }

    private static ProjectDto MapToDto(Project p, Dictionary<Guid, Category>? catDict = null)
    {
        catDict ??= new Dictionary<Guid, Category>();
        var catName = catDict.TryGetValue(p.CategoryId, out var cat) ? cat.Name : null;
        return new ProjectDto
        {
            Id = p.Id, Title = p.Title, Slug = p.Slug, ShortDescription = p.ShortDescription,
            Content = p.Content, CategoryId = p.CategoryId.ToString(), CategoryName = catName,
            Style = p.Style, Area = p.Area, Location = p.Location, Year = p.Year,
            ThumbnailUrl = p.ThumbnailUrl, IsFeatured = p.IsFeatured, Status = p.Status,
            SeoTitle = p.SeoTitle, SeoDescription = p.SeoDescription,
            CreatedAt = p.CreatedAt, PublishedAt = p.PublishedAt
        };
    }

    private static string GenerateSlug(string title)
    {
        var slug = title.ToLowerInvariant().Replace("đ", "d").Replace("ă", "a").Replace("â", "a").Replace("ê", "e").Replace("ô", "o").Replace("ư", "u").Replace(" ", "-");
        return new string(slug.Where(c => char.IsLetterOrDigit(c) || c == '-').ToArray());
    }
}
