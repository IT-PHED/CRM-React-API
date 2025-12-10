using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using SharedKernel;

namespace Application.Complaints.QueryDeptRegionAndStatus;

internal sealed class QueryDeptRegionAndStatusQueryHandler(IComplaintService complaintService) : IQueryHandler<QueryDeptRegionAndStatusQuery, PagedResponse<CrmComplaintDto>>
{
    public async Task<Result<PagedResponse<CrmComplaintDto>>> Handle(QueryDeptRegionAndStatusQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<CrmComplaintDto> complaints = await complaintService.QueryComplaintRegionAndStatus(query.departmentId, query.region ?? "0", query.status);

            return Result.Success(new PagedResponse<CrmComplaintDto>
            {
                Data = complaints,
                TotalItems = complaints.Count,
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<PagedResponse<CrmComplaintDto>>(Domain.Common.CommonErrors.CustomErrorMessage("Failed to fetch complaints"));
        }
    }
}
