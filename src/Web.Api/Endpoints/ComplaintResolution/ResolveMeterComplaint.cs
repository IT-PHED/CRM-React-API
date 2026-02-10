
using Application.Abstractions.Messaging;
using Application.ComplaintResolution.ResolveNoPowerComplaint;
using Application.Complaints.GetComplaintById;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.ComplaintResolution;

public class ResolveMeterComplaint : IEndpoint
{
    public sealed record Request(string feedback);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("complaint-resolution/resolve-complaint/{complaintId:guid}", async (Guid complaintId, [FromBody] Request request, ICommandHandler<ResolveNoPowerComplaintCommand, GetComplaintByIdQueryResponse> handler, CancellationToken cancellationToken) =>
        {
            var command = new ResolveNoPowerComplaintCommand(complaintId, request.feedback);

            Result<GetComplaintByIdQueryResponse> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<GetComplaintByIdQueryResponse>.Success(value, "Complaint Resolved successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.ComplaintResolution).RequireAuthorization().WithDescription("Resolve Meter complaint");
    }
}
