using FluentValidation;
using Shared.Domain.Dtos;

namespace Catalog.API.Validators;

public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
{
    public PaginationRequestValidator()
    {
        RuleFor(pg => pg.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        RuleFor(pg => pg.PageIndex).GreaterThanOrEqualTo(1).LessThanOrEqualTo(int.MaxValue);
    }
}
