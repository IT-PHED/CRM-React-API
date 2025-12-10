using Application.Abstractions.Messaging;

namespace Application.Complaints.ReassignComplaint;

public sealed record ReassignComplaintCommand(
    string TicketId,
    string ConsumerId,
    string AssignStaffId,
    string AssignEmail) : ICommand<string>;
