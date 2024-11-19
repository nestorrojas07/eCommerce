using FluentValidation;
using Order.API.Request;

namespace Order.API.Validators;

public class AddOrderDetailRequestValidator : AbstractValidator<AddOrderDetailRequest>
{
    public AddOrderDetailRequestValidator()
    {
        RuleFor(p => p.ProductId)
            .NotNull() 
            .GreaterThan(0)
            .LessThan(long.MaxValue);

        RuleFor(p => p.Quatity).NotNull().GreaterThan(0);
    }
}
