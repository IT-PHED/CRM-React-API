using Application.Abstractions.Factory.Complaint;
using Application.Abstractions.Messaging;
using Application.Complaints.Dto;
using SharedKernel;

namespace Application.Complaints.GetComplaintByTicket;

internal sealed class GetTicketByTicketQueryHandler(IComplaintService complaintService) : IQueryHandler<GetTicketByTicketQuery, ConsumerComplaintDto>
{
    public async Task<Result<ConsumerComplaintDto>> Handle(GetTicketByTicketQuery query, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(query.ticketNumber))
        {
            return Result.Failure<ConsumerComplaintDto>(Domain.Common.CommonErrors.CustomErrorMessage("ticket number is required"));
        }

        return await complaintService.GetComplaintByTicketNumber(query.ticketNumber);
    }
}
