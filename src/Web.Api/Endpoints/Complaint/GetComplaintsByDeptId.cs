using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Application.Complaints.GetComplaintByDepartmentId;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class GetComplaintsByDeptId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("complaint/department/{Id}", async (
            string Id,
            [AsParameters] PagedQuery fields,
            IQueryHandler<GetComplaintByDepartmentIdQuery, PagedResponse<CrmComplaintDto>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new GetComplaintByDepartmentIdQuery(Id, fields.pageNumber, fields.pageSize, fields.dateFrom, fields.dateTo);

            Result<PagedResponse<CrmComplaintDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<PagedResponse<CrmComplaintDto>>.Success(value, $"Fetched a Ticket by Department Id {Id}")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}
