using Application.Abstractions.Factory.Dashboard;
using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Dashboard.GetSlaCountSummary;

internal sealed class GetSlaCountSummaryQueryHandler(IDashboardService dashboardService) : IQueryHandler<GetSlaCountSummaryQuery, IEnumerable<DashboardDto>>
{
    public async Task<Result<IEnumerable<DashboardDto>>> Handle(GetSlaCountSummaryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            List<DashboardDto> response = await dashboardService.GetSlaCountSummary(query.fromDate, query.toDate, query.divId);
            return response.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<IEnumerable<DashboardDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Sla Count Summary"));
        }
    }
}
