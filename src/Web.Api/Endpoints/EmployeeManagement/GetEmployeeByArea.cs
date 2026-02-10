using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetEmployeeByArea;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetEmployeeByArea : IEndpoint
{
    internal sealed record Request(string Ibc, string? Bsc = null);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/employee-area", async ([AsParameters] Request request, IQueryHandler<GetEmployeeByAreaQuery, IEnumerable<EmployeeByIbcBscDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetEmployeeByAreaQuery(request.Ibc, request.Bsc);

            Result<IEnumerable<EmployeeByIbcBscDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EmployeeByIbcBscDto>>.Success(value, "All Employee by area")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get employee by Area");
    }
}
