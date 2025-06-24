using FLP.Application.Requests.Bugs;
using FluentValidation;

namespace FLP.Application.Validators.Bugs;

internal class GetBugsValidator : AbstractValidator<GetBugsPaginatedRequest>
{
    public GetBugsValidator()
    {
        RuleFor(f => f.Page)
            .GreaterThan(0)
            .WithMessage("Page number must be greater than 0.");
        RuleFor(f => f.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0.")
            .LessThan(100)
            .WithMessage("Page size must be less than 100");
    }
}
