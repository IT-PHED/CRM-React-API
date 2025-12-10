using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using SharedKernel;

namespace Application.Complaints.GetAllComplaints;

internal sealed class GetAllComplaintsQueryHandler(IComplaintService complaintService) : IQueryHandler<GetAllComplaintsQuery, PagedResponse<CrmComplaintDto>>
{
    public async Task<Result<PagedResponse<CrmComplaintDto>>> Handle(GetAllComplaintsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<CrmComplaintDto> complaints = await complaintService.GetAllComplaints(
                query.pageNumber,
                query.PageSize ?? 2000,
                query.searchTerm,
                query.DateFrom,
                query.DateTo);

            return Result.Success(new PagedResponse<CrmComplaintDto>
            {
                Data = complaints,
                PageNumber = query.pageNumber,
                PageSize = query.PageSize,
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
