using Application.Abstractions.Messaging;

namespace Application.Complaints.CreateMeterComplaints;

public sealed record CreateMeterComplaintCommand(
     string ConsumerNumber,
     string ComplaintTypeId,
     string ComplaintSubTypeId,
     string Source,
     string Priority,
     string Email,
     string? AssignToStaffId,
     string? AssignToEmail,
     string DepartmentId,
     string MobileNumber,
     int? correctMeterReading = 0,
     string? Remark = "",
     string? File = null,
     string? MediaLink = null
) : ICommand<string>;
