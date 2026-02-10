using Application.Abstractions.Factory.SLA;
using Application.Abstractions.Messaging;
using Application.Sla.Dto;
using SharedKernel;

namespace Application.Sla.GetSlaReports;

internal sealed class GetSlaReportQueryHandler(ISlaService slaService) : IQueryHandler<GetSlaReportQuery, IEnumerable<SlaDto>>
{
    public async Task<Result<IEnumerable<SlaDto>>> Handle(GetSlaReportQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<SlaDto>? reports = await slaService.GetSLAReport(query.ibc, query.bsc, query.category, query.subCategory, query.fromDate, query.toDate);

            if (reports is null)
            {
                return Result.Failure<IEnumerable<SlaDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch the SLA Reports"));
            }

            return reports.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<SlaDto>>(Domain.Common.CommonErrors.CustomErrorMessage("failed to fetch the SLA Reports"));
        }
    }
}
