using Application.Sla.Dto;

namespace Application.Abstractions.Factory.SLA;

public interface ISlaService
{
    Task<IReadOnlyList<SlaDto>> GetSLAReport(string ibc, string bsc, string category, string subCategory, DateTime fromDate, DateTime toDate);
}
