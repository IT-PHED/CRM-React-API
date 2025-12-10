using Application.Dashboard.Dto;

namespace Application.Abstractions.Factory.Dashboard;

public interface IDashboardService
{
    Task<List<DashboardDto>> GetTicketSummary(string? divId = "0", DateTime? fromDate = null, DateTime? toDate = null);
    Task<List<DashboardDto>> GetSlaCountSummary(DateTime fromDate, DateTime toDate, string? divId = "0");
    Task<List<DashboardDto>> GetCategoryWiseSummary(DateTime fromDate, DateTime toDate, string? divId = "0");
    Task<List<DashboardDto>> GeteDateWiseSummary(DateTime fromDate, DateTime toDate, string? divId = "0");
    Task<List<DashboardDto>> GeteLocationWiseSummary(DateTime fromDate, DateTime toDate, string? divId = "0");
    Task<List<DashboardDto>> GetSlaDIVCountSummary(DateTime fromDate, DateTime toDate, string? divId = "0");
    Task<List<DashboardDto>> GetSlaDurationSummary(DateTime fromDate, DateTime toDate, string? divId = "0");
}
