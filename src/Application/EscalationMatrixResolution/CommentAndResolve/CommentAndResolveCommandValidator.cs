using Application.EscalationMatrixResolution.Dto;
using FluentValidation;

namespace Application.EscalationMatrixResolution.CommentAndResolve;

public class CommentAndResolveCommandValidator : AbstractValidator<CommentAndResolveCommand>
{
    public CommentAndResolveCommandValidator()
    {
        RuleFor(x => x.payload)
                .NotNull().WithMessage("Payload cannot be null")
                .NotEmpty().WithMessage("At least one resolution item is required")
                .Must(x => x.Count <= 100).WithMessage("Cannot process more than 100 items at once");

        RuleForEach(x => x.payload)
            .SetValidator(new EscalationMatrixResolutionDtoValidator());
    }
}

public class EscalationMatrixResolutionDtoValidator : AbstractValidator<EscalationMatrixResolutionDto>
{
    public EscalationMatrixResolutionDtoValidator()
    {
        RuleFor(x => x.TICKET)
                .NotEmpty().WithMessage("Ticket number is required")
                .MaximumLength(50).WithMessage("Ticket number cannot exceed 50 characters")
                .Matches(@"^[A-Z0-9\-_]+$").WithMessage("Ticket number can only contain alphanumeric characters, hyphens, and underscores");

        RuleFor(x => x.EMPID)
            .NotEmpty().WithMessage("Employee ID is required")
            .MaximumLength(100).WithMessage("Employee ID cannot exceed 100 characters");
    }
}
