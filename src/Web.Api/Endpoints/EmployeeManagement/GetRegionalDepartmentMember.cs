using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetRegionalDepartmentMembers;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetRegionalDepartmentMember : IEndpoint
{
    internal sealed record Request(string DepartmentId, string AccountNumber);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/regional-department-member", async ([AsParameters] Request request, IQueryHandler<GetRegionalDepartmentMembersQuery, IEnumerable<UserRegionalProfileDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetRegionalDepartmentMembersQuery(request.DepartmentId, request.AccountNumber);

            Result<IEnumerable<UserRegionalProfileDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<UserRegionalProfileDto>>.Success(value, "Regional Department Members")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get Regional Department Members");
    }
}
