using FluentValidation;
using Order.API.Request;
using Shared.Domain.Dtos;

namespace Order.API.Validators;

public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
{
    public UpdateOrderRequestValidator()
    {
        RuleFor(p => p.CustomerName).NotEmpty().MinimumLength(1).MaximumLength(150);
        RuleFor(p => p.CustomerEmail)
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(150)
            .EmailAddress();
    }
}
