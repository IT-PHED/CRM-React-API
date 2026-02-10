using Application.Abstractions.Factory;
using Application.Complaints.Dto;
using Domain.Complaint;
using SharedKernel;

namespace Application.Complaints.CreateMeterComplaints;

internal sealed class NotifyAssignedEmailEventHandler(
    IRazorViewToString razorViewToString,
    IEmailService emailService) : IDomainEventHandler<NotifyAssignedEmailEvent>
{
    public async Task Handle(NotifyAssignedEmailEvent domainEvent, CancellationToken cancellationToken)
    {

        (string email, string ticketId, string assigneeName, DateTime submissionDate, string priority, string complaintType, string submittedBy, string complainantEmail, string complainantPhone, string remark) = domainEvent;

        var emailModel = new ComplaintNotificationEmailModel
        {
            ComplaintId = ticketId,
            AssigneeName = assigneeName,
            SubmissionDate = submissionDate,
            Priority = priority,
            ComplaintType = complaintType,
            SubmittedBy = submittedBy,
            ComplainantEmail = complainantEmail,
            ComplainantPhone = complainantPhone,
            ComplaintDescription = remark
        };

        string emailBody = await razorViewToString.RenderViewToStringAsync("/Views/RazorEmailTemplate/NotifyAssignedEmail.cshtml", emailModel);

        await emailService.SendCommonEmail(assigneeName, email, emailBody, new List<string> { }, "New Complaint Notification");
    }
}
