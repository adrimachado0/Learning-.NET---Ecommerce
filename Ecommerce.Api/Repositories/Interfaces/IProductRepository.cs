using Ecommerce.Api.Models;

namespace Ecommerce.Api.Repositories.Interfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(
        int page,
        int limit,
        string? categoria,
        string? term,
        decimal? min,
        decimal? max
    );
    Task<Product?> GetByIdAsync(int id);
    void Add(Product product);
    void Update(Product product);
    Task<int> DeleteAsync(int id);
    Task SaveChangesAsync();
}
