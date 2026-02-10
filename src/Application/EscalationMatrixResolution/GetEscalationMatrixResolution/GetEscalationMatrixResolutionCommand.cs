using Application.Abstractions.Messaging;
using Application.EscalationMatrixResolution.Dto;

namespace Application.EscalationMatrixResolution.GetEscalationMatrixResolution;

public sealed record GetEscalationMatrixResolutionCommand(string Uid, string ticket) : ICommand<IEnumerable<EscalationMatrixResolutionDto>>;
