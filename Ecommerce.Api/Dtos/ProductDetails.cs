using Ecommerce.Api.Models;

namespace Ecommerce.Api.Dtos;

public record ProductDetailsDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    DateOnly CreatedAt,
    Category Category
);