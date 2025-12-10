using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using SharedKernel;

namespace Application.Complaints.GetComplaintByDeptIdAndStatus;
internal class GetComplaintByDeptIdAndStatusQueryHandler(IComplaintService complaintService) : IQueryHandler<GetComplaintByDeptIdAndStatusQuery, PagedResponse<CrmComplaintDto>>
{
    public async Task<Result<PagedResponse<CrmComplaintDto>>> Handle(GetComplaintByDeptIdAndStatusQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<CrmComplaintDto> complaints = await complaintService.GetAllDepartmentComplaintAndStatus(query.departmentId, query.status, query.pageNumber, query.pageSize, query.dateFrom, query.dateTo);

            return Result.Success(new PagedResponse<CrmComplaintDto>
            {
                Data = complaints,
                PageNumber = query.pageNumber,
                PageSize = query.pageSize,
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
