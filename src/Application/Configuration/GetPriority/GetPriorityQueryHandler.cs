using Application.Abstractions.Factory.Configuration;
using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Configuration.GetPriority;

internal sealed class GetPriorityQueryHandler(IConfigurationService configurationService) : IQueryHandler<GetPriorityQuery, IEnumerable<EnumsResponseDto>>
{
    public async Task<Result<IEnumerable<EnumsResponseDto>>> Handle(GetPriorityQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<EnumsResponseDto> subTypes = await configurationService.GetPriorities();
            return subTypes.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<IEnumerable<EnumsResponseDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Priority"));
        }
    }
}
