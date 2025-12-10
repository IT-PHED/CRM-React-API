using Application.Area.Dto;
using Application.Configuration.Dto;

namespace Application.Abstractions.Factory.Area;

public interface IAreaService
{
    Task<AreaResponseDto> GetAreaAsync();
    Task<IReadOnlyList<EnumsResponseDto>> GetRegions();
}
