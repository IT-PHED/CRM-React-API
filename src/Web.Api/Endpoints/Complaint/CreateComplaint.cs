using Application.Abstractions.Messaging;
using Application.Complaints.CreateMeterComplaints;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class CreateComplaint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("complaint", async ([FromBody] CreateMeterComplaintRequest request,
            ICommandHandler<CreateMeterComplaintCommand, string> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateMeterComplaintCommand(
                request.ConsumerNumber,
                request.ComplaintTypeId,
                request.ComplaintSubTypeId,
                request.Source,
                request.Priority,
                request.Email,
                request.AssignToStaffId,
                request.AssignToEmail,
                request.DepartmentId,
                request.MobileNumber,
                request.CorrectMeterReading,
                request.Remark,
                request.File,
                request.MediaLink);

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<string>.Success(value, "Complaint loggedin successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}
