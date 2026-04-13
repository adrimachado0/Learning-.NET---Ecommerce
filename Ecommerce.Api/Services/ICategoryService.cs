using Ecommerce.Api.Dtos;

namespace Ecommerce.Api.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetCategoriesAsync();
    Task<CategoryDto?> GetCategoryByIdAsync(int id);
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto createCategory);
    Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updateCategory);
    Task<bool> DeleteCategoryByIdAsync(int id);
}
