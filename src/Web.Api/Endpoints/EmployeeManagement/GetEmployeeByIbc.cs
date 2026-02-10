using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetEmployeeByIBC;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetEmployeeByIbc : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/ibc/{ibc}", async (string ibc, IQueryHandler<GetEmployeesByIbcQuery, IEnumerable<EmployeeByIbcBscDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetEmployeesByIbcQuery(ibc);

            Result<IEnumerable<EmployeeByIbcBscDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EmployeeByIbcBscDto>>.Success(value, "All Employee by ibc")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get Employee By Ibc");
    }
}
