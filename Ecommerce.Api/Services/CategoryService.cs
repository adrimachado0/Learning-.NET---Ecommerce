using Ecommerce.Api.Dtos;
using Ecommerce.Api.Models;
using Ecommerce.Api.Repositories.Interfaces;

namespace Ecommerce.Api.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto(
            c.Id,
            c.Name
        )).ToList();
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if( category is null )
            return null;

        return new CategoryDto(
            category.Id,
            category.Name
        );
    }

    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto newCategory)
    {
        Category category = new()
        {
            Name = newCategory.Name  
        };

        categoryRepository.Add(category);
        await categoryRepository.SaveChangesAsync();
        
        return new CategoryDto(
            category.Id,
            category.Name
        );
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updatedCategory)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if( category is null ) 
            return null;

        category.Name = updatedCategory.Name;
        
        categoryRepository.Update(category);
        await categoryRepository.SaveChangesAsync();
        
        return new CategoryDto(
            category.Id,
            category.Name
        );
    }

    public async Task<bool> DeleteCategoryByIdAsync(int id)
    {
        var rowsAffected = await categoryRepository.DeleteAsync(id);
        return rowsAffected > 0;
    }
}
