using Application.Customers.Dto;

namespace Application.Abstractions.Factory.Consumer;

public interface IConsumerService
{
    Task<Domain.Consumer.Consumer> GetByComplaintIdAsync(string complaintId);
    Task<Domain.Consumer.Consumer> GetByIdAsync(string consumerId);
    Task<IReadOnlyList<ConsumerDto>> GetConsumerBySearchCriteria(string? ConsumerName = null, string? ConsumerNumber = null, string? MeterNumber = null, string? MobileNumber = null, string? PrevTicketNumber = null);
}
