using Ecommerce.Api.Data;
using Ecommerce.Api.Models;
using Ecommerce.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Repositories;

public class ProductRepository(EcommerceContext dbContext) : IProductRepository
{
    public async Task<IEnumerable<Product>> GetAllAsync(
        int page, 
        int limit, 
        string? categoria, 
        string? term, 
        decimal? min, 
        decimal? max
    )
    {
        var query = dbContext.Products.AsQueryable();

        if(!string.IsNullOrEmpty(categoria))
            query = query.Where(p => EF.Functions.ILike(p.Category.Name, categoria));

        if(!string.IsNullOrEmpty(term))
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{term}%"));

        if(min.HasValue)
            query = query.Where(p => p.Price >= min.Value);
  
        if(max.HasValue)
            query = query.Where(p => p.Price <= max.Value);

        var products = await query
            .Include(p => p.Category)
            .OrderBy(p => p.Id)
            .Skip((page - 1) * limit)            
            .Take(limit)
            .AsNoTracking()
            .ToListAsync();

        return products;
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    public void Add(Product product)
    {
        dbContext.Products.Add(product);
    }

    public void Update(Product product)
    {
        dbContext.Products.Update(product);
    }

    public async Task<int> DeleteAsync(int id)
    {
        var rowsAffected = await dbContext.Products
            .Where(product => product.Id == id)
            .ExecuteDeleteAsync();
        
        return rowsAffected;
    }

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }
}
