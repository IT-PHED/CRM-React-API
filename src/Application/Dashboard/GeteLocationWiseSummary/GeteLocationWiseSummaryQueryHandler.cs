using Application.Abstractions.Factory.Dashboard;
using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Dashboard.GeteLocationWiseSummary;
internal sealed class GeteLocationWiseSummaryQueryHandler(IDashboardService dashboardService) : IQueryHandler<GeteLocationWiseSummaryQuery, IEnumerable<DashboardDto>>
{
    public async Task<Result<IEnumerable<DashboardDto>>> Handle(GeteLocationWiseSummaryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            List<DashboardDto> response = await dashboardService.GeteLocationWiseSummary(query.fromDate, query.toDate, query.divId);
            return response.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<IEnumerable<DashboardDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Date Wise Summary"));
        }
    }
}
