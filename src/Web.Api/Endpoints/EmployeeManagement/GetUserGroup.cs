using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetUserGroup;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetUserGroup : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/user-group", async (IQueryHandler<GetUserGroupQuery, UserGroupDto> handler, CancellationToken cancellationToken, [FromQuery] string? StaffId = null) =>
        {
            var query = new GetUserGroupQuery(StaffId);

            Result<UserGroupDto> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<UserGroupDto>.Success(value, "User Group")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).RequireAuthorization().WithDescription("Get User Group Information");
    }
}
