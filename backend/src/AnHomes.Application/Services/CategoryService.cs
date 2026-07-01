using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;
using AnHomes.Application.Interfaces;

namespace AnHomes.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _uow;

    public CategoryService(IUnitOfWork uow) => _uow = uow;

    public async Task<IReadOnlyList<CategoryDto>> GetCategoriesByTypeAsync(string type, CancellationToken ct = default)
    {
        var all = await _uow.Categories.GetAllAsync(ct);
        return all.Where(c => c.Type == type && c.IsActive).OrderBy(c => c.SortOrder).Select(MapToDto).ToList();
    }

    public async Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken ct = default)
    {
        var all = await _uow.Categories.GetAllAsync(ct);
        return all.OrderBy(c => c.SortOrder).Select(MapToDto).ToList();
    }

    private static CategoryDto MapToDto(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Slug = c.Slug,
        Type = c.Type,
        Description = c.Description
    };
}
