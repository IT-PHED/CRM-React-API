using Application.EscalationMatrixResolution.Dto;

namespace Application.Abstractions.Factory.EscalationMatrixResolution;

public interface IEscalationMatrixResolutionService
{
    string decodeString(string base64Encoded);
    Task<IReadOnlyList<EscalationMatrixResolutionDto>> GetSLAForTicket(string ticket);
    Task<IReadOnlyList<EscalationMatrixResolutionDto>> CommentAndGetSLA(EscalationMatrixResolutionDto model);
    Task<IReadOnlyList<EscalationMatrixResolutionDto>> GetMyOpenSLATickets(string uid);
}
