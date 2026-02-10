using Application.Abstractions.Factory.Dashboard;
using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Dashboard.GetMonthlyStat;

internal sealed class GetMonthlyStatQueryHandler(IDashboardService dashboardService) : IQueryHandler<GetMonthlyStatQuery, ComplaintMonthlyStat>
{
    public async Task<Result<ComplaintMonthlyStat>> Handle(GetMonthlyStatQuery query, CancellationToken cancellationToken)
    {
        try
        {
            ComplaintMonthlyStat response = await dashboardService.CRM_MONTHLY_DEPT_STATS(query.departmentId, query.regionId);
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<ComplaintMonthlyStat>(CommonErrors.CustomErrorMessage("Failed to fetch Complaint monthly data"));
        }
    }
}
