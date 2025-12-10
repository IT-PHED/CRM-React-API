using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;

namespace Application.Dashboard.GetCategoryWiseSummary;

public sealed record GetCategoryWiseSummaryQuery(DateTime fromDate, DateTime toDate, string? divId = null) : IQuery<IEnumerable<DashboardDto>>;

