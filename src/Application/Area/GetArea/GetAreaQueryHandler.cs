using Application.Abstractions.Factory.Area;
using Application.Abstractions.Messaging;
using Application.Area.Dto;
using Domain.Common;
using SharedKernel;

namespace Application.Area.GetArea;

internal sealed class GetAreaQueryHandler(IAreaService areaService) : IQueryHandler<GetAreaQuery, AreaResponseDto>
{
    public async Task<Result<AreaResponseDto>> Handle(GetAreaQuery query, CancellationToken cancellationToken)
    {
        try
        {
            AreaResponseDto response = await areaService.GetAreaAsync();
            return response;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return Result.Failure<AreaResponseDto>(CommonErrors.CustomErrorMessage("Failed to fetch Area"));
        }
    }
}
