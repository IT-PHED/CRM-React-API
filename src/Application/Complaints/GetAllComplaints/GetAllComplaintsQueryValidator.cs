using System.Text.RegularExpressions;
using FluentValidation;

namespace Application.Complaints.GetAllComplaints;

public class GetAllComplaintsQueryValidator : AbstractValidator<GetAllComplaintsQuery>
{
    public GetAllComplaintsQueryValidator()
    {
        RuleFor(x => x.pageNumber)
         .NotEmpty()
         .WithMessage("Page number is required")
         .GreaterThan(0)
         .WithMessage("Page number must be greater than 0")
         .LessThanOrEqualTo(1000)
         .WithMessage("Page number cannot exceed 1000");

        // PageSize validation
        RuleFor(x => x.PageSize)
            .NotNull()
            .WithMessage("Page size is required")
            .InclusiveBetween(1, 2000)
            .WithMessage("Page size must be between 1 and 2000");

        // SearchTerm validation
        RuleFor(x => x.searchTerm)
            .MaximumLength(200)
            .WithMessage("Search term cannot exceed 200 characters")
            .Must(BeValidSearchTerm)
            .WithMessage("Search term contains invalid characters")
            .When(x => !string.IsNullOrWhiteSpace(x.searchTerm));

        // Date validation
        RuleFor(x => x.DateFrom)
            .Must(date => date >= new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .WithMessage("DateFrom cannot be earlier than 2015-01-01")
            .When(x => x.DateFrom.HasValue);

        RuleFor(x => x.DateTo)
            .Must(date => date >= new DateTime(2015, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            .WithMessage("DateTo cannot be earlier than 2000-01-01")
            .When(x => x.DateTo.HasValue);

        // Date range validation
        When(x => x.DateFrom.HasValue && x.DateTo.HasValue, () => RuleFor(x => x.DateTo)
                .Must((model, dateTo) => dateTo >= model.DateFrom)
                .WithMessage("DateTo must be greater than or equal to DateFrom"));

    }

    private bool BeValidSearchTerm(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        { return true; }

        // Allow alphanumeric, spaces, hyphens, underscores, and common special characters
        var regex = new Regex(@"^[a-zA-Z0-9\s\-_@.#&()+]*$");
        return regex.IsMatch(searchTerm);
    }
}
