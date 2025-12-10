using Application.Abstractions.Factory.Dashboard;
using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Dashboard.GetCategoryWiseSummary;

internal sealed class GetCategoryWiseSummaryQueryHandler(IDashboardService dashboardService) : IQueryHandler<GetCategoryWiseSummaryQuery, IEnumerable<DashboardDto>>
{
    public async Task<Result<IEnumerable<DashboardDto>>> Handle(GetCategoryWiseSummaryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            List<DashboardDto> response = await dashboardService.GetCategoryWiseSummary(query.fromDate, query.toDate, query.divId);
            return response.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<IEnumerable<DashboardDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Category Wise Summary"));
        }
    }
}
