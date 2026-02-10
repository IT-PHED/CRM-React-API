using SharedKernel;

namespace Domain.Complaint;

public sealed record CloseOutComplaintDomainEvent(
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
) : IDomainEvent;
