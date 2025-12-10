using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Application.Complaints.GetComplaintByDepartmentId;
using Application.Complaints.GetComplaintByDeptIdAndStatus;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class GetComplaintsAndDeptIdAndStatus : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("complaint/department/{Id}/status", async (
           string Id,
           [AsParameters] PagedQuery fields,
           IQueryHandler<GetComplaintByDeptIdAndStatusQuery, PagedResponse<CrmComplaintDto>> handler,
           CancellationToken cancellationToken,
           [FromQuery] string status) =>
        {
            var query = new GetComplaintByDeptIdAndStatusQuery(status, Id, fields.pageNumber, fields.pageSize, fields.dateFrom, fields.dateTo);

            Result<PagedResponse<CrmComplaintDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<PagedResponse<CrmComplaintDto>>.Success(value, $"Fetched a Ticket by Department Id {Id} And Status")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}
