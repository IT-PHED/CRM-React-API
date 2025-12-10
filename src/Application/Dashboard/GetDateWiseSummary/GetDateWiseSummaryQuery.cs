using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;

namespace Application.Dashboard.GetDateWiseSummary;

public sealed record GetDateWiseSummaryQuery(DateTime fromDate, DateTime toDate, string? divId = null) : IQuery<IEnumerable<DashboardDto>>;
