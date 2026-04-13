using Ecommerce.Api.Data;
using Ecommerce.Api.Dtos;
using Ecommerce.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Services;

public class ProductService(
    EcommerceContext dbContext,
    ICategoryService categoryService
) : IProductService
{
    public async Task<List<ProductSummaryDto>> GetPaginatedProductsAsync(ProductFilterQueryDto filters)
    {
        var query = dbContext.Products.AsQueryable();

        if(!string.IsNullOrEmpty(filters.Categoria))
            query = query.Where(p => EF.Functions.ILike(p.Category.Name, filters.Categoria));

        if(!string.IsNullOrEmpty(filters.Term))
            query = query.Where(p => EF.Functions.ILike(p.Name, $"%{filters.Term}%"));

        if(filters.Min.HasValue)
            query = query.Where(p => p.Price >= filters.Min.Value);
  
        if(filters.Max.HasValue)
            query = query.Where(p => p.Price <= filters.Max.Value);

        var products = await query
            .OrderBy(p => p.Id)
            .Skip((filters.Page - 1) * filters.Limit)            
            .Take(filters.Limit)
            .Select(p => new ProductSummaryDto(
                p.Id,
                p.Name,
                p.Price,
                p.ImageUrl,
                p.CreatedAt,
                p.Category.Name
            ))
            .AsNoTracking()
            .ToListAsync();

        return products;
    }

    public async Task<ProductDetailsDto?> GetProductByIdAsync(int id)
    {
        var product = await dbContext.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        if(product is null) return null;
        return new ProductDetailsDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.ImageUrl,
            product.CreatedAt,
            product.Category
        );
    }

    public async Task<ProductDetailsDto> CreateProductAsync(CreateProductDto newProduct)
    {
        var categoryFound = await categoryService.GetCategoryByIdAsync(newProduct.CategoryId);
        if(categoryFound is null) 
            throw new KeyNotFoundException($"La categoría {newProduct.CategoryId} no existe.");

        Product product = new()
        {
            Name = newProduct.Name,
            Description = newProduct.Description,
            Price = newProduct.Price,
            ImageUrl = newProduct.ImageUrl,
            CategoryId = newProduct.CategoryId
        };

        dbContext.Add(product);
        await dbContext.SaveChangesAsync();

        return new ProductDetailsDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.ImageUrl,
            product.CreatedAt,
            product.Category
        );
    }

    public async Task<ProductDetailsDto?> UpdateProductAsync(int id, UpdateProductDto updatedProduct)
    {
        var categoryFound = await categoryService.GetCategoryByIdAsync(updatedProduct.CategoryId);
        if(categoryFound is null) 
            throw new KeyNotFoundException($"La categoría {updatedProduct.CategoryId} no existe.");

        var product = await dbContext.Products.FindAsync(id);

        if( product is null )
            return null;

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.ImageUrl = updatedProduct.ImageUrl;
        product.CategoryId = updatedProduct.CategoryId;

        await dbContext.SaveChangesAsync();
        return new ProductDetailsDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.ImageUrl,
            product.CreatedAt,
            product.Category
        );
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var rowsAffected = await dbContext.Products
            .Where(product => product.Id == id)
            .ExecuteDeleteAsync();

        return rowsAffected > 0;
    }
}
