using Application.Abstractions.Messaging;
using Application.Dashboard.Dto;

namespace Application.Dashboard.GetTicketSummary;

public sealed record
    GetTicketSummaryQuery(string? divId = null, DateTime? dateFrom = null, DateTime? dateTo = null) : IQuery<IEnumerable<DashboardDto>>;
