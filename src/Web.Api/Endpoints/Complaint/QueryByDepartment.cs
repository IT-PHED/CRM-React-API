
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Application.Complaints.QueryDepartmentById;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public sealed class QueryByDepartment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("complaint/query-department", async (
           [AsParameters] QueryDepartmentByIdQuery fields,
           IQueryHandler<QueryDepartmentByIdQuery, PagedResponse<CrmComplaintDto>> handler,
           CancellationToken cancellationToken) =>
        {
            var query = new QueryDepartmentByIdQuery(fields.departmentId, fields.searchTerm, fields.pageNumber, fields.pageSize, fields.dateFrom, fields.dateTo);

            Result<PagedResponse<CrmComplaintDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<PagedResponse<CrmComplaintDto>>.Success(value, $"Fetched all department tickets {fields.departmentId}")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints);
    }
}
