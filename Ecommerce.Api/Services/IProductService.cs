using Ecommerce.Api.Dtos;

namespace Ecommerce.Api.Services;

public interface IProductService
{
    Task<List<ProductSummaryDto>> GetPaginatedProductsAsync(ProductFilterQueryDto filters);
    Task<ProductDetailsDto?> GetProductByIdAsync(int id);
    Task<ProductDetailsDto> CreateProductAsync(CreateProductDto newProduct);
    Task<ProductDetailsDto?> UpdateProductAsync(int id, UpdateProductDto updatedProduct);
    Task<bool> DeleteProductAsync(int id);
}
