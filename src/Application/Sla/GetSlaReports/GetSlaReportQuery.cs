using Application.Abstractions.Messaging;
using Application.Sla.Dto;

namespace Application.Sla.GetSlaReports;

public sealed record GetSlaReportQuery(string ibc, string bsc, string category, string subCategory, DateTime fromDate, DateTime toDate)
    : IQuery<IEnumerable<SlaDto>>;
