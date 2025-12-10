using Application.Abstractions.Factory.Dashboard;
using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Dashboard.GetTicketSummary;
internal sealed class GetTicketSummaryQueryHandler(IDashboardService dashboardService) : IQueryHandler<GetTicketSummaryQuery, IEnumerable<DashboardDto>>
{
    public async Task<Result<IEnumerable<DashboardDto>>> Handle(GetTicketSummaryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            List<DashboardDto> response = await dashboardService.GetTicketSummary(query.divId, query.dateFrom, query.dateFrom);
            return response.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<IEnumerable<DashboardDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Ticket Summary"));
        }
    }
}
