using Ecommerce.Api.Dtos;
using FluentValidation;

namespace Ecommerce.Api.Validators;

public class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del producto no puede estar vacío.")
            .MinimumLength(5).WithMessage("El nombre del producto debe tener al menos 5 caracteres.")
            .MaximumLength(50).WithMessage("El nombre del producto es muy largo.");
        
        RuleFor(x => x.Description)
            .MinimumLength(20).WithMessage("La descripción es muy corta.")
            .MaximumLength(150).WithMessage("La descripción es demasiado larga.")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("El precio no puede ser negativo.")
            .LessThanOrEqualTo(150).WithMessage("El precio máximo es $150.");
            
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Debe seleccionar una categoría válida.");

        RuleFor(x => x.ImageUrl)
            .NotEmpty().WithMessage("La URL de la imagen no puede estar en blanco si se envía.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .WithMessage("El formato de la URL de la imagen no es válido.")
            .When(x => x.ImageUrl != null);
    }
}
