using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetGroupMembers;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetGroupMembers : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/group-members/{GroupId:int}", async (int GroupId, IQueryHandler<GetGroupMembersQuery, IEnumerable<UserRegionalProfileDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetGroupMembersQuery(GroupId);

            Result<IEnumerable<UserRegionalProfileDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<UserRegionalProfileDto>>.Success(value, "Get Group Members")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get group members");
    }
}
