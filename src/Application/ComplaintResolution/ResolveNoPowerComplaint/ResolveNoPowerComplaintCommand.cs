using Application.Abstractions.Messaging;
using Application.Complaints.GetComplaintById;

namespace Application.ComplaintResolution.ResolveNoPowerComplaint;

public sealed record ResolveNoPowerComplaintCommand(Guid ComplaintId, string feedback) : ICommand<GetComplaintByIdQueryResponse>;
