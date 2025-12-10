using Application.Abstractions.Messaging;
using Application.Complaints.Dto;

namespace Application.Complaints.GetComplaintByTicket;

public sealed record GetTicketByTicketQuery(string ticketNumber) : IQuery<ConsumerComplaintDto>;

