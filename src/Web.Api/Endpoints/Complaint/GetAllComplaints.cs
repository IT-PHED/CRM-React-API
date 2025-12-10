using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Application.Complaints.GetAllComplaints;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Complaint;

public class GetAllComplaints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("complaint", async ([AsParameters] GetAllComplaintsQuery fields, IQueryHandler<GetAllComplaintsQuery, PagedResponse<CrmComplaintDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetAllComplaintsQuery(fields.pageNumber, fields.PageSize, fields.searchTerm, fields.DateFrom, fields.DateTo);

            Result<PagedResponse<CrmComplaintDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<PagedResponse<CrmComplaintDto>>.Success(value, "All Complaints")),
                 error => CustomResults.Problem(error)
            );
        }).WithTags(Tags.Complaints).RequireAuthorization();
    }
}
