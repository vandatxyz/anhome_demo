using AnHomes.Application.Dtos;

namespace AnHomes.Application.Interfaces;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryDto>> GetCategoriesByTypeAsync(string type, CancellationToken ct = default);
    Task<IReadOnlyList<CategoryDto>> GetAllAsync(CancellationToken ct = default);
}
