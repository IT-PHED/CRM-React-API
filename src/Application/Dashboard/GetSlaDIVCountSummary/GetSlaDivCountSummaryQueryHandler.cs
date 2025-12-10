using Application.Abstractions.Factory.Dashboard;
using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Dashboard.GetSlaDIVCountSummary;


internal sealed class GetSlaDivCountSummaryQueryHandler(IDashboardService dashboardService) : IQueryHandler<GetSlaDivCountSummaryQuery, IEnumerable<DashboardDto>>
{
    public async Task<Result<IEnumerable<DashboardDto>>> Handle(GetSlaDivCountSummaryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            List<DashboardDto> response = await dashboardService.GetSlaDIVCountSummary(query.fromDate, query.toDate, query.divId);
            return response.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<IEnumerable<DashboardDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Date Wise Summary"));
        }
    }
}
