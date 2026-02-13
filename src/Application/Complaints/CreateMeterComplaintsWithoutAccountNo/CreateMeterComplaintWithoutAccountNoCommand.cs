using Application.Abstractions.Messaging;

namespace Application.Complaints.CreateMeterComplaintsWithoutAccountNo;

public sealed record CreateMeterComplaintWithoutAccountNoCommand(
     string ComplaintTypeId,
     string ComplaintSubTypeId,
     string Source,
     string Priority,
     string Email,
     string MobileNumber,
     string Remark,
     string Customer_name,
     string Address,
     string Type,
     string RegionId,
     string DepartmentId
) : ICommand<string>;
