using Application.Abstractions.Messaging;
using Application.ComplaintResolution.Dto;
using Application.ComplaintResolution.ResolveWrongMeterReadingComplaint;
using Application.Complaints.GetComplaintById;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.ComplaintResolution;

internal sealed class ResolveWrongMeterReadingComplaint : IEndpoint
{
    internal sealed record Request(
     Guid ComplaintId,
     int CorrectMeterReading,
     int BillingMonth,
     int BillingYear);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch("complaint-resolution/resolve-wrong-meter-reading-complaint", async ([FromBody] Request request, ICommandHandler<ResolveWrongMeterReadingComplaintCommand, CalculatedWrongMeterComplaintDto> handler, CancellationToken cancellationToken) =>
        {
            var command = new ResolveWrongMeterReadingComplaintCommand(request.ComplaintId, request.CorrectMeterReading, request.BillingMonth, request.BillingYear);

            Result<CalculatedWrongMeterComplaintDto> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<CalculatedWrongMeterComplaintDto>.Success(value, "Wrong Meter reading complaint successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.ComplaintResolution).RequireAuthorization().WithDescription("Resolve Wrong Meter reading");
    }
}
