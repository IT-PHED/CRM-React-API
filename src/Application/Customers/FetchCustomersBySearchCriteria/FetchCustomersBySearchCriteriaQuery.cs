using Application.Abstractions.Messaging;
using Application.Customers.Dto;

namespace Application.Customers.FetchCustomersBySearchCriteria;

public sealed record FetchCustomersBySearchCriteriaQuery(
    string? ConsumerName = null,
    string? ConsumerNumber = null,
    string? MeterNumber = null,
    string? MobileNumber = null,
    string? PrevTicketNumber = null) : IQuery<IEnumerable<ConsumerDto>>;
