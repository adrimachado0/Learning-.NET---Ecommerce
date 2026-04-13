using Ecommerce.Api.Dtos;
using FluentValidation;

namespace Ecommerce.Api.Validators;

public class ProductFilterQueryValidator : AbstractValidator<ProductFilterQueryDto>
{
    public ProductFilterQueryValidator()
    {
        Include(new PaginationValidator());
        
        RuleFor(x => x.Term)
            .MaximumLength(50).WithMessage("El término de búsqueda no puede ser tan largo")
            .Must(t => string.IsNullOrWhiteSpace(t) || t.Trim().Length >= 3)
            .WithMessage("Si buscas por término, escribe al menos 3 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Term));

        RuleFor(x => x.Categoria)
            .MaximumLength(30).WithMessage("El nombre de la categoría es demasiado largo.")
            .When(x => !string.IsNullOrEmpty(x.Categoria));

        RuleFor(x => x.Min)
            .GreaterThanOrEqualTo(0).WithMessage("El precio mínimo no puede ser negativo");

        RuleFor(x => x.Max)
            .GreaterThan(0).WithMessage("El precio máximo debe ser mayor a 0.")
            .LessThanOrEqualTo(150).WithMessage("El precio máximo no puede exceder los 150.");

        RuleFor(x => x.Min)
            .LessThan(x => x.Max)
            .WithMessage("El precio mínimo debe ser menor al máximo.")
            .When(x => x.Min.HasValue && x.Max.HasValue);
    }
}
