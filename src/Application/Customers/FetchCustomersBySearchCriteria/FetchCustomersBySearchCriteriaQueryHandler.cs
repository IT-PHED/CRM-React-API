using Application.Abstractions.Factory.Consumer;
using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Application.Customers.Dto;
using Dapper;
using Domain.Common;
using SharedKernel;

namespace Application.Customers.FetchCustomersBySearchCriteria;

internal sealed class FetchCustomersBySearchCriteriaQueryHandler(IConsumerService consumerService) : IQueryHandler<FetchCustomersBySearchCriteriaQuery, IEnumerable<ConsumerDto>>
{
    public async Task<Result<IEnumerable<ConsumerDto>>> Handle(FetchCustomersBySearchCriteriaQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<ConsumerDto> consumers = await consumerService.GetConsumerBySearchCriteria(query.ConsumerName, query.ConsumerNumber, query.MeterNumber, query.MobileNumber, query.PrevTicketNumber);
            return consumers.AsList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<IEnumerable<ConsumerDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Consumers"));
        }
    }
}
