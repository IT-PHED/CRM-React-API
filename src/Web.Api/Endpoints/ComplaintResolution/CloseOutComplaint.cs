using Application.Abstractions.Messaging;
using Application.ComplaintResolution.CloseNoPowerComplaint;
using Application.Complaints.GetComplaintById;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.ComplaintResolution;

internal sealed class CloseOutComplaint : IEndpoint
{
    internal sealed record Request(string Feedback);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("complaint-resolution/close-complaint/{complaintId:guid}", async (Guid complaintId, [FromBody] Request request, ICommandHandler<CloseNoPowerComplaintCommand, GetComplaintByIdQueryResponse> handler, CancellationToken cancellationToken) =>
        {
            var command = new CloseNoPowerComplaintCommand(complaintId, request.Feedback);

            Result<GetComplaintByIdQueryResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<GetComplaintByIdQueryResponse>.Success(value, "Complaint Closed successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.ComplaintResolution).RequireAuthorization().WithDescription("Close Customer complaint");
    }
}
