using Application.Abstractions.Messaging;
using Application.EscalationMatrixResolution.Dto;

namespace Application.EscalationMatrixResolution.MyOpenTickets;

public sealed record MyOpenTicketsCommand(string Uid) : ICommand<IEnumerable<EscalationMatrixResolutionDto>>;
