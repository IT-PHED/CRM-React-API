using Application.Abstractions.Messaging;
using Application.Customers.Dto;
using Application.Customers.FetchCustomersBySearchCriteria;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customer;

public class GetCustomerBySearchFilter : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customer/search-filter", async ([AsParameters] FetchCustomersBySearchCriteriaQuery fields, IQueryHandler<FetchCustomersBySearchCriteriaQuery, IEnumerable<ConsumerDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new FetchCustomersBySearchCriteriaQuery(fields.ConsumerName, fields.ConsumerNumber, fields.MeterNumber, fields.MobileNumber, fields.PrevTicketNumber);

            Result<IEnumerable<ConsumerDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<ConsumerDto>>.Success(value, "Get Customer by filter")),
                 error => CustomResults.Problem(error)
            );

        }).WithTags(Tags.Customer).WithDescription("Fetch Customer by query filter");
    }
}
