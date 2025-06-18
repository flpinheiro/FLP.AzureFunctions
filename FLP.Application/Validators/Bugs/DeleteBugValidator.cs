using FLP.Application.Requests.Bugs;
using FluentValidation;

namespace FLP.Application.Validators.Bugs;

internal class DeleteBugValidator : AbstractValidator<DeleteBugRequest>
{
    public DeleteBugValidator()
    {
        RuleFor(f => f.Id)
            .NotEmpty()
            .WithMessage("Id is required.")
            .Must(id => id != Guid.Empty)
            .WithMessage("Id must be a valid GUID and cannot be an empty GUID.");
    }
}