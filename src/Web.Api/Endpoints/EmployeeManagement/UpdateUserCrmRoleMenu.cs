using Application.Abstractions.Messaging;
using Application.EmployeeManagement.UpdateUserCrmRole;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.EmployeeManagement;

public class UpdateUserCrmRoleMenu : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("employees/update-user-crm-role", async ([FromBody] UpdateUserCrmRoleCommand request, ICommandHandler<UpdateUserCrmRoleCommand, object> handler, CancellationToken cancellationToken) =>
        {
            var command = new UpdateUserCrmRoleCommand(request.StaffId, request.NewCRMRole);

            Result<object> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<object>.Success(value, "User Crm Role Updated successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.EmployeeManagement).RequireAuthorization().WithDescription("Update User CRM Role Information");
    }
}
