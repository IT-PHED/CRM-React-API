using Application.Abstractions.Messaging;
using Application.EscalationMatrixResolution.Dto;

namespace Application.EscalationMatrixResolution.CommentAndResolve;

public sealed record CommentAndResolveCommand(List<EscalationMatrixResolutionDto> payload) : ICommand<IEnumerable<EscalationMatrixResolutionDto>>;
