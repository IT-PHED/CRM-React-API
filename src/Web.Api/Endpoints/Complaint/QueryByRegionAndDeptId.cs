using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Application.Complaints.QueryDeptRegionAndStatus;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class QueryByRegionAndDeptId : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("complaint/region/{regionId}", async (
            string regionId,
            [FromQuery] string status,
            [FromQuery] string deptId,
            IQueryHandler<QueryDeptRegionAndStatusQuery, PagedResponse<CrmComplaintDto>> handler,
            CancellationToken cancellationToken) =>
        {
            var query = new QueryDeptRegionAndStatusQuery(deptId, status, regionId);

            Result<PagedResponse<CrmComplaintDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<PagedResponse<CrmComplaintDto>>.Success(value, $"Fetched a Tickets by Region Id, department Id And Status")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}
