using FluentValidation;

namespace Application.Customers.FetchCustomersBySearchCriteria;

public class FetchCustomersBySearchCriteriaQueryValidator : AbstractValidator<FetchCustomersBySearchCriteriaQuery>
{
    public FetchCustomersBySearchCriteriaQueryValidator()
    {
        RuleFor(x => x)
            .Must(HaveAtLeastOneSearchParameter)
            .WithMessage("At least one search parameter must be provided. Please provide one of: ConsumerName, ConsumerNumber, MeterNumber, MobileNumber, or PrevTicketNumber.");
    }

    private bool HaveAtLeastOneSearchParameter(FetchCustomersBySearchCriteriaQuery query)
    {
        return !string.IsNullOrWhiteSpace(query.ConsumerName) ||
               !string.IsNullOrWhiteSpace(query.ConsumerNumber) ||
               !string.IsNullOrWhiteSpace(query.MeterNumber) ||
               !string.IsNullOrWhiteSpace(query.MobileNumber) ||
               !string.IsNullOrWhiteSpace(query.PrevTicketNumber);
    }
}
