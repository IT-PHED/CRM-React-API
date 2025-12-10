using Application.Abstractions.Factory.Area;
using Application.Abstractions.Messaging;
using Application.Configuration.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Area.GetRegions;

internal sealed class GetRegionsQueryHandler(IAreaService areaService) : IQueryHandler<GetRegionsQuery, IEnumerable<EnumsResponseDto>>
{
    public async Task<Result<IEnumerable<EnumsResponseDto>>> Handle(GetRegionsQuery query, CancellationToken cancellationToken)
    {
        try
        {
            IReadOnlyList<EnumsResponseDto> response = await areaService.GetRegions();
            return response.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<IEnumerable<EnumsResponseDto>>(CommonErrors.CustomErrorMessage("Failed to fetch Regions"));
        }

    }
}
