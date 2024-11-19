using FluentValidation;
using Order.API.Request;

namespace Order.API.Validators;

public class UpdateOrderDetailRequestValidator : AbstractValidator<UpdateOrderDetailRequest>
{
    public UpdateOrderDetailRequestValidator()
    {
        RuleFor(p => p.Id)
            .NotNull() 
            .GreaterThan(0)
            .LessThan(long.MaxValue);

        RuleFor(p => p.Quatity).NotNull().GreaterThan(0);
    }
}
