using Application.Abstractions.Factory.EscalationMatrixResolution;
using Application.Abstractions.Messaging;
using Application.EscalationMatrixResolution.Dto;
using SharedKernel;

namespace Application.EscalationMatrixResolution.CommentAndResolve;

internal sealed class CommentAndResolveCommandHandler(IEscalationMatrixResolutionService escalationMatrixResolutionService) : ICommandHandler<CommentAndResolveCommand, IEnumerable<EscalationMatrixResolutionDto>>
{
    public async Task<Result<IEnumerable<EscalationMatrixResolutionDto>>> Handle(CommentAndResolveCommand command, CancellationToken cancellationToken)
    {
        bool closeAll = false;
        var processedResults = new List<EscalationMatrixResolutionDto>();

        try
        {
            if (command.payload.Count > 1)
            {
                closeAll = true;
            }

            foreach (EscalationMatrixResolutionDto item in command.payload)
            {
                if (IsBase64String(item.EMPID))
                {
                    item.EMPID = escalationMatrixResolutionService.decodeString(item.EMPID);
                }

                if (closeAll)
                {
                    item.ShouldCloseTicket = 1;
                }
                IReadOnlyList<EscalationMatrixResolutionDto> output = await escalationMatrixResolutionService.CommentAndGetSLA(item);

                if (output == null || !output.Any())
                {
                    return Result.Failure<IEnumerable<EscalationMatrixResolutionDto>>(
                            Domain.Common.CommonErrors.CustomErrorMessage(
                                $"No results returned for ticket"));
                }

                processedResults.AddRange(output);
            }

            return processedResults.Any()
                    ? Result.Success<IEnumerable<EscalationMatrixResolutionDto>>(processedResults)
                    : Result.Failure<IEnumerable<EscalationMatrixResolutionDto>>(
                        Domain.Common.CommonErrors.CustomErrorMessage("No escalation matrix resolutions found"));

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<IEnumerable<EscalationMatrixResolutionDto>>(Domain.Common.CommonErrors.CustomErrorMessage("Something went wrong, Ticket and Uid parameter may be wrong"));
        }
    }

    public bool IsBase64String(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length % 4 != 0)
        {
            return false;
        }

        Span<byte> buffer = stackalloc byte[s.Length];
        return Convert.TryFromBase64String(s, buffer, out _);
    }
}
