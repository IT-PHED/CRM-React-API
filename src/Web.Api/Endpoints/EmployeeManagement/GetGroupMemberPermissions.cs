using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetGroupMemberPermissions;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetGroupMemberPermissions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/group-member-permissions/user/{userId}", async (string userId, IQueryHandler<GetGroupMemberPermissionsQuery, CrmUserPermissionDto> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetGroupMemberPermissionsQuery(userId);

            Result<CrmUserPermissionDto> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<CrmUserPermissionDto>.Success(value, "Group Member permission")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get group members Permissions");
    }
}
