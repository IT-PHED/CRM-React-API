using FluentValidation;

namespace Application.EmployeeManagement.GetEmployeeByArea;

public class GetEmployeeByAreaQueryValidator : AbstractValidator<GetEmployeeByAreaQuery>
{
    public GetEmployeeByAreaQueryValidator()
    {
        RuleFor(x => x.Ibc)
            .NotEmpty()
            .WithMessage("IBC code is required.")
            .MaximumLength(50)
            .WithMessage("IBC code must not exceed 50 characters.");

        When(x => x.Bsc != null, () => RuleFor(x => x.Bsc!)
                .NotEmpty()
                .WithMessage("BSC cannot be empty if provided.")
                .MaximumLength(50)
                .WithMessage("BSC must not exceed 50 characters."));
    }
}
