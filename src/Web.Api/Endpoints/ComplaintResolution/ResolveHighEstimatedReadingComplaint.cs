using Application.Abstractions.Messaging;
using Application.ComplaintResolution.Dto;
using Application.ComplaintResolution.ResolveHighEstimatedReadingComplaint;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.ComplaintResolution;

public class ResolveHighEstimatedReadingComplaint : IEndpoint
{
    internal sealed record Request(string consumerId, double EstimatedReading, DateTime MonthFrom, DateTime MonthTo);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("complaint-resolution/resolve-high-estimated-reading-complaint", async (
            [FromBody] Request request,
            ICommandHandler<ResolveHighEstimatedReadingComplaintCommand, CalculatedHighEstimateComplaintDto> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new ResolveHighEstimatedReadingComplaintCommand(request.consumerId, request.EstimatedReading, request.MonthFrom, request.MonthTo);

            Result<CalculatedHighEstimateComplaintDto> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<CalculatedHighEstimateComplaintDto>.Success(value, "Resolved High Estimated Reading Complaint successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.ComplaintResolution).RequireAuthorization().WithDescription("Resolve High Estimated Reading Complaint");
    }
}
