using Ecommerce.Api.Dtos;
using FluentValidation;

namespace Ecommerce.Api.Validators;

public class PaginationValidator : AbstractValidator<PaginationDto>
{
    public PaginationValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0)
            .WithMessage("La página debe ser un número entero mayor a 0.");

        RuleFor(x => x.Limit)
            .InclusiveBetween(1, 100)
            .WithMessage("El límite debe ser un número entero entre 1 y 100");
    }
}
