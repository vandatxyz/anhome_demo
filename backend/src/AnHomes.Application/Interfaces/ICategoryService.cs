using AnHomes.Application.Dtos;

namespace AnHomes.Application.Interfaces;

public interface ICategoryService
{
 Task<IReadOnlyList<CategoryDto>> GetCategoriesByTypeAsync(string type, CancellationToken ct = default);
 Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken ct = default);
 Task<CategoryDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
 Task<CategoryDto> CreateAsync(CreateCategoryRequest request, CancellationToken ct = default);
 Task<CategoryDto?> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default);
 Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}