using Application.Abstractions.Factory.EscalationMatrixResolution;
using Application.Abstractions.Messaging;
using Application.EscalationMatrixResolution.Dto;
using SharedKernel;

namespace Application.EscalationMatrixResolution.MyOpenTickets;

internal sealed class MyOpenTicketsCommandHandler(IEscalationMatrixResolutionService escalationMatrixResolutionService) : ICommandHandler<MyOpenTicketsCommand, IEnumerable<EscalationMatrixResolutionDto>>
{
    public async Task<Result<IEnumerable<EscalationMatrixResolutionDto>>> Handle(MyOpenTicketsCommand command, CancellationToken cancellationToken)
    {
        string uid = escalationMatrixResolutionService.decodeString(command.Uid);
        try
        {
            IReadOnlyList<EscalationMatrixResolutionDto> output = await escalationMatrixResolutionService.GetMyOpenSLATickets(uid);

            return output.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<EscalationMatrixResolutionDto>>(Domain.Common.CommonErrors.CustomErrorMessage($"Something went wrong, Received Uid: {uid} Ticket and Uid."));
        }
    }
}
