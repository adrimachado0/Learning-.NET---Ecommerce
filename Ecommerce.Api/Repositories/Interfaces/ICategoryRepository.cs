using Ecommerce.Api.Models;

namespace Ecommerce.Api.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    void Add(Category category);
    void Update(Category category);
    Task<int> DeleteAsync(int id);
    
    // La pieza clave para tu regla de negocio:
    Task<bool> HasProductsAsync(int categoryId);
    Task SaveChangesAsync();
}