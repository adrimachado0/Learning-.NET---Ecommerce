using Ecommerce.Api.Data;
using Ecommerce.Api.Dtos;
using Ecommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Services;

public class CategoryService(EcommerceContext dbContext) : ICategoryService
{
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var categories = await dbContext.Categories
            .Select(c => new CategoryDto(
                c.Id,
                c.Name
            ))
            .ToListAsync();

        return categories;
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
    {
        var category = await dbContext.Categories.FindAsync(id);

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

        dbContext.Categories.Add(category);
        await dbContext.SaveChangesAsync();
        
        return new CategoryDto(
            category.Id,
            category.Name
        );
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto updatedCategory)
    {
        var category = await dbContext.Categories.FindAsync(id);
        if( category is null ) 
            return null;

        category.Name = updatedCategory.Name;
        await dbContext.SaveChangesAsync();
        
        return new CategoryDto(
            category.Id,
            category.Name
        );
    }

    public async Task<bool> DeleteCategoryByIdAsync(int id)
    {
        var rowsAffected = await dbContext.Categories
            .Where(c => c.Id == id)
            .ExecuteDeleteAsync();

        return rowsAffected > 0;
    }
}
