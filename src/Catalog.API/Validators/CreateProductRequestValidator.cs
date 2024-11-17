using Catalog.API.Request.Product;
using FluentValidation;

namespace Catalog.API.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(1).MaximumLength(150);
        RuleFor(p => p.Description).NotEmpty().MinimumLength(1).MaximumLength(500);
        RuleFor(p => p.Price).NotNull().GreaterThanOrEqualTo(0);
    }
}
