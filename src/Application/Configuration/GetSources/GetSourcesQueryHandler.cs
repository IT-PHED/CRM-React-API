using Application.Abstractions.Factory.Configuration;
using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Configuration.GetSources;

internal sealed class GetSourcesQueryHandler(IConfigurationService configurationService) : IQueryHandler<GetSourcesQuery, IEnumerable<EnumsResponseDto>>
{
    public async Task<Result<IEnumerable<EnumsResponseDto>>> Handle(GetSourcesQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<EnumsResponseDto> subTypes = await configurationService.GetSources();
            return subTypes.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return Result.Failure<IEnumerable<EnumsResponseDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Source"));
        }
    }
}
