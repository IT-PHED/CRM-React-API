using Application.Abstractions.Messaging;
using Application.Sla.Dto;
using Application.Sla.GetSlaReports;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.SLA;

public class Sla : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("sla/reports", async ([AsParameters] GetSlaReportQuery request, IQueryHandler<GetSlaReportQuery, IEnumerable<SlaDto>> handler, CancellationToken cancellationToken) =>
        {
            var query = new GetSlaReportQuery(request.ibc, request.bsc, request.category, request.subCategory, request.fromDate, request.toDate);

            Result<IEnumerable<SlaDto>> result = await handler.Handle(query, cancellationToken);

            return result.Match(
                 value => Results.Ok(ApiResponse<IEnumerable<SlaDto>>.Success(value, "All SLA Report")),
                 error => CustomResults.Problem(error)
            );

        }).WithTags(Tags.SLA).WithDescription("Fetch all the SLA Reports");
    }
}
