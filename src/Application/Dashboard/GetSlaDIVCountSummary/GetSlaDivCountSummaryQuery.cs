using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;

namespace Application.Dashboard.GetSlaDIVCountSummary;

public sealed record GetSlaDivCountSummaryQuery(DateTime fromDate, DateTime toDate, string? divId = null) : IQuery<IEnumerable<DashboardDto>>;
