using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;
using AnHomes.Application.Interfaces;

namespace AnHomes.Application.Services;

public class ServiceService : IServiceService
{
    private readonly IUnitOfWork _uow;

    public ServiceService(IUnitOfWork uow) => _uow = uow;

    public async Task<PagedResult<ServiceDto>> GetPublishedServicesAsync(int page, int pageSize, CancellationToken ct = default)
    {
        var all = await _uow.Services.GetAllAsync(ct);
        var query = all.Where(s => s.Status == "published").AsQueryable();
        var total = query.Count();
        var items = query.OrderBy(s => s.SortOrder).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<ServiceDto> { Data = items.Select(MapToDto).ToList(), Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<ServiceDto?> GetPublishedServiceBySlugAsync(string slug, CancellationToken ct = default)
    {
        var services = await _uow.Services.FindAsync(s => s.Slug == slug && s.Status == "published", ct);
        var service = services.FirstOrDefault();
        return service == null ? null : MapToDto(service);
    }

    public async Task<PagedResult<ServiceDto>> GetAllServicesAsync(int page, int pageSize, string? status = null, CancellationToken ct = default)
    {
        var all = await _uow.Services.GetAllAsync(ct);
        var query = all.AsQueryable();
        if (!string.IsNullOrEmpty(status)) query = query.Where(s => s.Status == status);
        var total = query.Count();
        var items = query.OrderByDescending(s => s.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        return new PagedResult<ServiceDto> { Data = items.Select(MapToDto).ToList(), Total = total, Page = page, PageSize = pageSize };
    }

    public async Task<ServiceDto> CreateServiceAsync(Dtos.CreateServiceRequest request, CancellationToken ct = default)
    {
        var service = new Service
        {
            Title = request.Title,
            Slug = GenerateSlug(request.Title),
            ShortDescription = request.ShortDescription,
            Content = request.Content,
            Icon = request.Icon,
            IsFeatured = request.IsFeatured,
            SeoTitle = request.SeoTitle,
            SeoDescription = request.SeoDescription,
            Status = "draft"
        };
        await _uow.Services.AddAsync(service, ct);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(service);
    }

    public async Task<ServiceDto?> UpdateServiceAsync(Guid id, Dtos.UpdateServiceRequest request, CancellationToken ct = default)
    {
        var service = await _uow.Services.GetByIdAsync(id, ct);
        if (service == null) return null;
        service.Title = request.Title;
        service.ShortDescription = request.ShortDescription;
        service.Content = request.Content;
        service.Icon = request.Icon;
        service.IsFeatured = request.IsFeatured;
        service.SeoTitle = request.SeoTitle;
        service.SeoDescription = request.SeoDescription;
        service.Status = request.Status;
        service.UpdatedAt = DateTime.UtcNow;
        _uow.Services.Update(service);
        await _uow.SaveChangesAsync(ct);
        return MapToDto(service);
    }

    public async Task<bool> DeleteServiceAsync(Guid id, CancellationToken ct = default)
    {
        var service = await _uow.Services.GetByIdAsync(id, ct);
        if (service == null) return false;
        _uow.Services.Delete(service);
        await _uow.SaveChangesAsync(ct);
        return true;
    }

    private static ServiceDto MapToDto(Service s) => new()
    {
        Id = s.Id,
        Title = s.Title,
        Slug = s.Slug,
        ShortDescription = s.ShortDescription,
        Content = s.Content,
        Icon = s.Icon,
        ImageUrl = s.ImageUrl,
        IsFeatured = s.IsFeatured,
        Status = s.Status,
        SeoTitle = s.SeoTitle,
        SeoDescription = s.SeoDescription,
        CreatedAt = s.CreatedAt
    };

    private static string GenerateSlug(string title)
    {
        var slug = title.ToLowerInvariant().Replace("đ", "d").Replace("ă", "a").Replace("â", "a").Replace("ê", "e").Replace("ô", "o").Replace("ư", "u").Replace(" ", "-");
        return new string(slug.Where(c => char.IsLetterOrDigit(c) || c == '-').ToArray());
    }
}
