using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Complaints.ReassignComplaint;

public class ReassignComplaintCommandValidator : AbstractValidator<ReassignComplaintCommand>
{
    public ReassignComplaintCommandValidator()
    {
        RuleFor(x => x.TicketId)
             .NotEmpty().WithMessage("Ticket ID is required.")
             .MaximumLength(20).WithMessage("Ticket ID cannot exceed 20 characters.")
             .Must(BeNumeric).WithMessage("Ticket ID must contain only numbers.");

        RuleFor(x => x.ConsumerId)
            .NotEmpty().WithMessage("Consumer ID is required.")
            .MaximumLength(20).WithMessage("Consumer ID cannot exceed 20 characters.")
            .Must(BeNumeric).WithMessage("Consumer ID must contain only numbers.");

        RuleFor(x => x.AssignStaffId)
            .NotEmpty().WithMessage("Assign Staff ID is required.")
            .MaximumLength(7).WithMessage("Assign Staff ID cannot exceed 7 characters.");

        RuleFor(x => x.AssignEmail)
           .NotEmpty().WithMessage("Email is required.")
           .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.")
           .EmailAddress().WithMessage("Please provide a valid email address format.")
           .Must(EndsWithPhedDomain).WithMessage("Email must end with @phed.com.ng")
           .Must(BeValidPhedEmailFormat).WithMessage("Email format is invalid. Expected format: username@phed.com.ng");
    }

    private bool BeNumeric(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return false;
        }

        return value.All(char.IsDigit);
    }

    private bool EndsWithPhedDomain(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        return email.Trim().EndsWith("@phed.com.ng", StringComparison.OrdinalIgnoreCase);
    }

    private bool BeValidPhedEmailFormat(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        email = email.Trim();

        if (!EndsWithPhedDomain(email))
        {
            return false;
        }

        int atIndex = email.LastIndexOf('@');
        if (atIndex <= 0)
        {
            return false;
        }

        string localPart = email.Substring(0, atIndex);

        var localPartRegex = new Regex(@"^[a-zA-Z0-9._-]+$");

        return localPartRegex.IsMatch(localPart);
    }
}
