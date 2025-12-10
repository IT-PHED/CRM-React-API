using FluentValidation;

namespace Application.Complaints.CreateMeterComplaints;
public class CreateMeterComplaintCommandValidator : AbstractValidator<CreateMeterComplaintCommand>
{
    public readonly string[] priority = new[] { "Low", "Medium", "High" };

    public CreateMeterComplaintCommandValidator()
    {
        RuleFor(x => x.ConsumerNumber)
            .NotEmpty().WithMessage("Consumer number is required.")
            .MaximumLength(60).WithMessage("Consumer number must not exceed 60 characters.");

        RuleFor(x => x.ComplaintTypeId)
            .NotEmpty().WithMessage("Complaint type is required.")
            .MaximumLength(60).WithMessage("Complaint type ID must not exceed 60 characters.");

        RuleFor(x => x.ComplaintSubTypeId)
            .NotEmpty().WithMessage("Complaint subtype is required.")
            .MaximumLength(60).WithMessage("Complaint subtype ID must not exceed 60 characters.");

        RuleFor(x => x.Source)
            .NotEmpty().WithMessage("Source is required.")
            .MaximumLength(200).WithMessage("Source must not exceed 200 characters.");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .Must(p => priority.Contains(p, StringComparer.OrdinalIgnoreCase))
            .WithMessage("Priority must be 'Low', 'Medium', or 'High'.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email address is required.")
            .MaximumLength(60).WithMessage("Email must not exceed 60 characters.");

        RuleFor(x => x.AssignToEmail)
            .EmailAddress()
            .When(x => !string.IsNullOrWhiteSpace(x.AssignToEmail))
            .WithMessage("AssignToEmail must be a valid email address.");

        RuleFor(x => x.AssignToEmail)
            .MaximumLength(60)
            .When(x => !string.IsNullOrWhiteSpace(x.AssignToEmail))
            .WithMessage("AssignToEmail must not exceed 60 characters.");

        RuleFor(x => x.DepartmentId)
            .NotEmpty().WithMessage("Department ID is required.")
            .MaximumLength(50).WithMessage("Department ID must not exceed 50 characters.");

        RuleFor(x => x.MobileNumber)
            .NotEmpty().WithMessage("Mobile number is required.")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Mobile number must be a valid phone number (10-15 digits, optional + prefix).")
            .MaximumLength(60).WithMessage("Mobile number must not exceed 60 characters.");

        // Optional fields - basic checks
        RuleFor(x => x.Remark)
            .MaximumLength(2000).When(x => !string.IsNullOrEmpty(x.Remark))
            .WithMessage("Remark must not exceed 2000 characters.");

        RuleFor(x => x.File)
           .MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.File))
           .WithMessage("File path must not exceed 4000 characters.");

        RuleFor(x => x.MediaLink)
            .MaximumLength(4000).When(x => !string.IsNullOrEmpty(x.MediaLink))
            .WithMessage("Media link must not exceed 4000 characters.")
            .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrEmpty(x.MediaLink))
            .WithMessage("Media link must be a valid absolute URL.");
    }
}
