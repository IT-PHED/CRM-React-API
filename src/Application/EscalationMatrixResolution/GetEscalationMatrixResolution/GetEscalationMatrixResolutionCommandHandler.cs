using Application.Abstractions.Factory.EscalationMatrixResolution;
using Application.Abstractions.Messaging;
using Application.EscalationMatrixResolution.Dto;
using SharedKernel;

namespace Application.EscalationMatrixResolution.GetEscalationMatrixResolution;

internal sealed class GetEscalationMatrixResolutionCommandHandler(IEscalationMatrixResolutionService escalationMatrixResolutionService) : ICommandHandler<GetEscalationMatrixResolutionCommand, IEnumerable<EscalationMatrixResolutionDto>>
{
    public async Task<Result<IEnumerable<EscalationMatrixResolutionDto>>> Handle(GetEscalationMatrixResolutionCommand command, CancellationToken cancellationToken)
    {
        string uid = escalationMatrixResolutionService.decodeString(command.Uid);
        try
        {
            IReadOnlyList<EscalationMatrixResolutionDto> output = await escalationMatrixResolutionService.GetSLAForTicket(command.ticket);

            return output.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<EscalationMatrixResolutionDto>>(Domain.Common.CommonErrors.CustomErrorMessage($"Something went wrong, Received ticket: {command.ticket}, Uid: {uid} Ticket and Uid."));
        }
    }
}
