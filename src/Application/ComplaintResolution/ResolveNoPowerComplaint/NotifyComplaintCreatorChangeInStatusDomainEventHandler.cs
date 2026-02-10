using Application.Abstractions.Factory;
using Application.ComplaintResolution.Dto;
using Domain.Complaint;
using SharedKernel;

namespace Application.ComplaintResolution.ResolveNoPowerComplaint;

internal sealed class NotifyComplaintCreatorChangeInStatusDomainEventHandler(
    IRazorViewToString razorViewToString,
    IEmailService emailService) : IDomainEventHandler<NotifyComplaintCreatorChangeInStatusDomainEvent>
{
    public async Task Handle(NotifyComplaintCreatorChangeInStatusDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        (
                string staffName,
                string staffEmail,
                string complaintId,
                string category,
                string description,
                string status,
                string closedBy,
                string closedDate,
                string closingRemark) = domainEvent;

        var emailModel = new UpdateComplaintStatusEmailDto
        {
            Category = category,
            Description = description,
            Status = status,
            StaffName = staffName,
            ClosedBy = closedBy,
            ClosedDate = closedDate,
            ClosingRemark = closingRemark,
            ComplaintId = complaintId,
        };

        string emailBody = await razorViewToString.RenderViewToStringAsync("/Views/RazorEmailTemplate/NotifyComplaintCreatorChangeInStatus.cshtml", emailModel);

        await emailService.SendCommonEmail(staffName, staffEmail, emailBody, new List<string> { }, "Complaint has been approved!");
    }
}
