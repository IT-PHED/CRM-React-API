using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetGroupPrivileges;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetGroupPrivileges : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/group-privileges/{GroupId:int}", async (int GroupId, IQueryHandler<GetGroupPrivilegesQuery, IEnumerable<ScreenPrivilegeDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetGroupPrivilegesQuery(GroupId);

            Result<IEnumerable<ScreenPrivilegeDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<ScreenPrivilegeDto>>.Success(value, "Get Group Screen Privilege")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get group screen privileges");
    }
}
