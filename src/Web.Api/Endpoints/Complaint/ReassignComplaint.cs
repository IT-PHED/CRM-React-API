using Application.Abstractions.Messaging;
using Application.Complaints.ReassignComplaint;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class ReassignComplaint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("complaint/reassign", async (
            [FromBody] ReassignComplaintCommand payload,
            ICommandHandler<ReassignComplaintCommand, string> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ReassignComplaintCommand(payload.TicketId, payload.ConsumerId, payload.AssignStaffId, payload.AssignEmail);

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<string>.Success(value, "Complaint reassigned successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}
