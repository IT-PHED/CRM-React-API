using SharedKernel;

namespace Domain.Complaint;

public sealed record NotifyAssignedEmailEvent
    (string email, string ticketId, string assigneeName, DateTime submissionDate, string priority, string complaintType, string submittedBy, string complainantEmail, string complainantPhone, string remark) : IDomainEvent;

