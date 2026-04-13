using Ecommerce.Api.Models;

namespace Ecommerce.Api.Dtos;

public record ProductSummaryDto(
    int Id,
    string Name,
    decimal Price,
    string? ImageUrl,
    DateOnly CreatedAt,
    string CategoryName
);