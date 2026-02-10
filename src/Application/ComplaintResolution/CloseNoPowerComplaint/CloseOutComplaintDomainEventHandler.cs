using Application.Abstractions.Factory;
using Application.ComplaintResolution.Dto;
using Domain.Complaint;
using SharedKernel;

namespace Application.ComplaintResolution.CloseNoPowerComplaint;

internal sealed class CloseOutComplaintDomainEventHandler(
    IRazorViewToString razorViewToString,
    IEmailService emailService, ISmsService smsService) : IDomainEventHandler<CloseOutComplaintDomainEvent>
{
    public async Task Handle(CloseOutComplaintDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        (
        string ComplaintId,
        string StaffEmail,
        string StaffName,
        string CustomerEmail,
        string CustomerName,
        string CustomerPhone,
        string ComplaintTicket,
        string ComplaintDescription,
        DateTime ClosedDate,
        string Category,
        string Status,
        string ClosedBy,
        string ClosingRemark
       ) = domainEvent;

        var emailModel = new UpdateComplaintStatusEmailDto
        {
            Category = Category,
            Description = ComplaintDescription,
            Status = Status,
            StaffName = StaffName,
            ClosedBy = ClosedBy,
            ClosedDate = ClosedDate.ToString("R"),
            ClosingRemark = ClosingRemark,
            ComplaintId = ComplaintId,
        };

        var customerEmailModel = new CustomerCloseOutEmailViewModelDto
        {
            ClosedDate = ClosedDate,
            ComplaintDescription = ComplaintDescription,
            CustomerName = CustomerName,
            Ticket = ComplaintTicket

        };

        string staffEmailBody = await razorViewToString.RenderViewToStringAsync("/Views/RazorEmailTemplate/NotifyComplaintCreatorChangeInStatus.cshtml", emailModel);
        string customerEmailBody = await razorViewToString.RenderViewToStringAsync("/Views/RazorEmailTemplate/CustomerCloseOutEmail.cshtml", customerEmailModel);

        var tasks = new List<Task>
        {
            emailService.SendCommonEmail(StaffName, StaffEmail, staffEmailBody, new List<string> { }, "Complaint has been closed!")
        };

        string message = $"Your Complaint #{ComplaintTicket} Has Been Resolved";

        if (!string.IsNullOrEmpty(CustomerEmail))
        {
            tasks.Add(emailService.SendCommonEmail(CustomerName, CustomerEmail, customerEmailBody, new List<string> { }, message));
        }

        if (!string.IsNullOrEmpty(CustomerPhone))
        {
            tasks.Add(smsService.SendSms(message, CustomerPhone, cancellationToken));
        }

        await Task.WhenAll(tasks);
    }
}
