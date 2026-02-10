using SharedKernel;

namespace Domain.Complaint;

public sealed record NotifyComplaintCreatorChangeInStatusDomainEvent(
    string StaffName,
    string StaffEmail,
    string ComplaintId,
    string Category,
    string Description,
    string Status,
    string ClosedBy,
    string ClosedDate,
    string ClosingRemark) : IDomainEvent;
