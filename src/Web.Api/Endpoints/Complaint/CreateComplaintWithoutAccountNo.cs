using Application.Abstractions.Messaging;
using Application.Complaints.CreateMeterComplaints;
using Application.Complaints.CreateMeterComplaintsWithoutAccountNo;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;
namespace Web.Api.Endpoints.Complaint;

public class CreateComplaintWithoutAccountNo : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("complaintWithoutAccountNo", async ([FromBody] CreateRequestWithoutAccount request,
            ICommandHandler<CreateMeterComplaintWithoutAccountNoCommand, string> handler,
            CancellationToken cancellationToken) =>
        {
            var command = new CreateMeterComplaintWithoutAccountNoCommand(
                request.ComplaintTypeId,
                request.ComplaintSubTypeId,
                request.Source,
                request.Priority,
                request.Email,
                request.MobileNumber,
                request.Remark,
                request.CustomerName,
                request.Address,
                request.Type,
                request.RegionId,
                request.DepartmentId
                );

            Result<string> result = await handler.Handle(command, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<string>.Success(value, "Complaint loggedin successfully")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}
