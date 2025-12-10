using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;

namespace Application.Dashboard.GetSlaCountSummary;

public sealed record GetSlaCountSummaryQuery(DateTime fromDate, DateTime toDate, string? divId = null) : IQuery<IEnumerable<DashboardDto>>;
