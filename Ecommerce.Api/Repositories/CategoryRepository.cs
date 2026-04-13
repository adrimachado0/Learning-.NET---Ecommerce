using Ecommerce.Api.Data;
using Ecommerce.Api.Models;
using Ecommerce.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Repositories;

public class CategoryRepository(EcommerceContext dbContext) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        var categories = await dbContext.Categories
            .AsNoTracking()
            .ToListAsync();
        return categories;
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        return await dbContext.Categories.FindAsync(id);
    }

    public void Add(Category newCategory)
    {
        dbContext.Categories.Add(newCategory);
    }

    public void Update(Category updatedCategory)
    {
        dbContext.Categories.Update(updatedCategory);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var rowsAffected = await dbContext.Categories
            .Where(category => category.Id == id)
            .ExecuteDeleteAsync();
        return rowsAffected;
    }

    public Task<bool> HasProductsAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}