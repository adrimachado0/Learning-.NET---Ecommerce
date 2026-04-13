using Ecommerce.Api.Dtos;
using FluentValidation;

namespace Ecommerce.Api.Validators;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("El nombre de la categoría es muy corto")
            .MaximumLength(30).WithMessage("El nombre de la categoría es muy largo");
    }
}