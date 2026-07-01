using AnHomes.Application.Dtos;
using AnHomes.Application.Interfaces;
using AnHomes.Domain.Entities;

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

	public async Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
	{
		var c = await _uow.Categories.GetByIdAsync(id, ct);
		return c == null ? null : MapToDto(c);
	}

	public async Task<CategoryDto> CreateAsync(CreateCategoryRequest request, CancellationToken ct = default)
	{
		var category = new Category
		{
			Name = request.Name,
			Slug = request.Slug,
			Type = request.Type,
			Description = request.Description,
			SortOrder = request.SortOrder,
			IsActive = request.IsActive
		};
		await _uow.Categories.AddAsync(category, ct);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(category);
	}

	public async Task<CategoryDto?> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default)
	{
		var category = await _uow.Categories.GetByIdAsync(id, ct);
		if (category == null) return null;
		category.Name = request.Name;
		category.Slug = request.Slug;
		category.Type = request.Type;
		category.Description = request.Description;
		category.SortOrder = request.SortOrder;
		category.IsActive = request.IsActive;
		category.UpdatedAt = DateTime.UtcNow;
		_uow.Categories.Update(category);
		await _uow.SaveChangesAsync(ct);
		return MapToDto(category);
	}

	public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
	{
		var category = await _uow.Categories.GetByIdAsync(id, ct);
		if (category == null) return false;
		_uow.Categories.Delete(category);
		await _uow.SaveChangesAsync(ct);
		return true;
	}

	private static CategoryDto MapToDto(Category c) => new()
	{
		Id = c.Id,
		Name = c.Name,
		Slug = c.Slug,
		Type = c.Type,
		Description = c.Description,
		IsActive = c.IsActive,
		SortOrder = c.SortOrder
	};
}