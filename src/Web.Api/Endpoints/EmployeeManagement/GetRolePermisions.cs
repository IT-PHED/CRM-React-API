using Application.Abstractions.Messaging;
using Application.EmployeeManagement.Dto;
using Application.EmployeeManagement.GetRolePerms;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class GetRolePermisions : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("employees/role-menu", async (IQueryHandler<GetRolePermsQuery, IEnumerable<CrmRoleMenu>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetRolePermsQuery();

            Result<IEnumerable<CrmRoleMenu>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<CrmRoleMenu>>.Success(value, "All Crm Role Menu")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).WithDescription("Get Crm Role Menu");
    }
}
