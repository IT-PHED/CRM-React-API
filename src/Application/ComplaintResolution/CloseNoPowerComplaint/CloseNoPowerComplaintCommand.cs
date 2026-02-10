using Application.Abstractions.Messaging;
using Application.Complaints.GetComplaintById;

namespace Application.ComplaintResolution.CloseNoPowerComplaint;

public sealed record CloseNoPowerComplaintCommand(Guid ComplaintId, string Feedback) : ICommand<GetComplaintByIdQueryResponse>;
