using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;

namespace Application.Dashboard.GetSlaDurationSummary;

public sealed record GetSlaDurationSummaryQuery(DateTime fromDate, DateTime toDate, string? divId = null) : IQuery<IEnumerable<DashboardDto>>;
