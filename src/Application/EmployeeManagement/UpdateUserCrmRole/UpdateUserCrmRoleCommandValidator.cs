using FluentValidation;

namespace Application.EmployeeManagement.UpdateUserCrmRole;

public class UpdateUserCrmRoleCommandValidator : AbstractValidator<UpdateUserCrmRoleCommand>
{
    public UpdateUserCrmRoleCommandValidator()
    {
        RuleFor(x => x.StaffId)
            .NotEmpty().WithMessage("Staff ID is required.")
            .Length(5, 10).WithMessage("Staff ID must be 5-10 characters.");

        RuleFor(x => x.NewCRMRole).NotEmpty().WithMessage("CRM Role is required.");
    }
}
