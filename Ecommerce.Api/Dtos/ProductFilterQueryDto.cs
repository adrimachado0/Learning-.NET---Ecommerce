namespace Ecommerce.Api.Dtos;

public record ProductFilterQueryDto(
    string? Categoria,
    string? Term,
    decimal? Max,
    decimal? Min,
    int Page = 1,
    int Limit = 16
) : PaginationDto(Page, Limit);