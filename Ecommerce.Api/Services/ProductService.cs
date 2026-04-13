using Ecommerce.Api.Dtos;
using Ecommerce.Api.Models;
using Ecommerce.Api.Repositories.Interfaces;

namespace Ecommerce.Api.Services;

public class ProductService(
    IProductRepository productRepository,
    ICategoryRepository categoryRepository
) : IProductService
{
    public async Task<List<ProductSummaryDto>> GetPaginatedProductsAsync(ProductFilterQueryDto filters)
    {
        var products = await productRepository.GetAllAsync(
            filters.Page,
            filters.Limit,
            filters.Categoria,
            filters.Term,
            filters.Min,
            filters.Max
        );

        return products.Select(p => new ProductSummaryDto(
            p.Id,
            p.Name,
            p.Price,
            p.ImageUrl,
            p.CreatedAt,
            p.Category.Name
        )).ToList();
    }

    public async Task<ProductDetailsDto?> GetProductByIdAsync(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
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
        var categoryFound = await categoryRepository.GetByIdAsync(newProduct.CategoryId);
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

        productRepository.Add(product);
        await productRepository.SaveChangesAsync();

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
        var product = await productRepository.GetByIdAsync(id);
        if( product is null )
            return null;
        
        var categoryFound = await categoryRepository.GetByIdAsync(updatedProduct.CategoryId);
        if(categoryFound is null) 
            throw new KeyNotFoundException($"La categoría {updatedProduct.CategoryId} no existe.");


        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        product.ImageUrl = updatedProduct.ImageUrl;
        product.CategoryId = updatedProduct.CategoryId;

        productRepository.Update(product);
        await productRepository.SaveChangesAsync();

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
        var rowsAffected = await productRepository.DeleteAsync(id);
        return rowsAffected > 0;
    }
}
