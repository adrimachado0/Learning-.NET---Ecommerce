using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Dtos;

public record CreateProductDto(
    string Name,
    string Description,
    decimal Price,
    string? ImageUrl,
    int CategoryId
);