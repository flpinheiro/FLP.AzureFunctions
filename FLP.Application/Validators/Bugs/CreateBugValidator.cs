using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLP.Application.Requests.Bugs;
using FluentValidation;

namespace FLP.Application.Validators.Bugs;

internal class CreateBugValidator : AbstractValidator<CreateBugRequest>
{
    public CreateBugValidator()
    {
        RuleFor(f => f.Title)
            .MinimumLength(5)
            .WithMessage("Title must be at least 5 characters long.")
            .MaximumLength(50)
            .WithMessage("Title must not exceed 50 characters.")
            .NotEmpty()
            .WithMessage("Title is required.");

        RuleFor(f => f.Description)
            .MinimumLength(10)
            .WithMessage("Description must be at least 10 characters long.")
            .MaximumLength(500)
            .WithMessage("Description must not exceed 500 characters.")
            .NotEmpty()
            .WithMessage("Description is required.");

        //RuleFor(f => f.AssignedToUserId)
        //    .Must(id => id == null || id != Guid.Empty)
        //    .WithMessage("AssignedToUserId must be a valid GUID or null. If provided, it cannot be an empty GUID.");

    }
}
