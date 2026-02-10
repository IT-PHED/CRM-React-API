using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetEmployees;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetEmployees : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees", async (IQueryHandler<GetEmployeesQuery, IEnumerable<EmployeeDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetEmployeesQuery();

            Result<IEnumerable<EmployeeDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<EmployeeDto>>.Success(value, "All Employee")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get All Employees");
    }
}
