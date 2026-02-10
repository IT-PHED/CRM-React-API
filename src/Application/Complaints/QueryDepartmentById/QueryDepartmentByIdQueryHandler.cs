using System.Globalization;
using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using Application.Complaints.GetComplaintByDeptIdAndStatus;
using SharedKernel;

namespace Application.Complaints.QueryDepartmentById;
internal sealed class QueryDepartmentByIdQueryHandler(IComplaintService complaintService) : IQueryHandler<QueryDepartmentByIdQuery, PagedResponse<CrmComplaintDto>>
{
    public async Task<Result<PagedResponse<CrmComplaintDto>>> Handle(QueryDepartmentByIdQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<CrmComplaintDto> complaints = await complaintService.QueryComplaintByDepartment(query.departmentId, query.searchTerm, Convert.ToInt32(query.pageNumber, CultureInfo.InvariantCulture), Convert.ToInt32(query.pageSize, CultureInfo.InvariantCulture), query.dateFrom, query.dateTo);

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
