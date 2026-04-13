using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Api.Dtos;

public record UpdateProductDto(
    [Required][StringLength(30)] string Name,
    [Required][StringLength(120)] string Description,
    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero.")] 
    decimal Price,
    [Url(ErrorMessage = "Debes ingresar una URL válida (ej. https://...)")] 
    string? ImageUrl,
    [Required] int CategoryId
);
