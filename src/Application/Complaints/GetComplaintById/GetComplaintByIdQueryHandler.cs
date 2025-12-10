using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Messaging;
using Domain.Common;
using SharedKernel;

namespace Application.Complaints.GetComplaintById;

internal sealed class GetComplaintByIdQueryHandler(IComplaintService complaintService) : IQueryHandler<GetComplaintByIdQuery, GetComplaintByIdQueryResponse>
{
    public async Task<Result<GetComplaintByIdQueryResponse>> Handle(GetComplaintByIdQuery query, CancellationToken cancellationToken)
    {
        if (query.Id == Guid.Empty)
        {
            return Result.Failure<GetComplaintByIdQueryResponse>(CommonErrors.CustomErrorMessage("A valid Guid Id is required!"));
        }

        GetComplaintByIdQueryResponse responseBody = await complaintService.GetComplaintById<GetComplaintByIdQueryResponse>(query.Id.ToString());

        return responseBody;
    }
}
