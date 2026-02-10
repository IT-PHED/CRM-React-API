using Application.Abstractions.Messaging;
using Application.EmployeeManagement.GetRegions;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetRegions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/regions", async (IQueryHandler<GetRegionsQuery, IEnumerable<object>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetRegionsQuery();

            Result<IEnumerable<object>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<object>>.Success(value, "All Regions")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get Regions");
    }
}
