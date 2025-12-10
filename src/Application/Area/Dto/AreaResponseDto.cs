using Application.Configuration.Dto;

namespace Application.Area.Dto;

public class CategoryDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}

public class BscDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string IbcId { get; set; }
}

public class AreaResponseDto
{
    public List<EnumsResponseDto> IBCs { get; set; } = new List<EnumsResponseDto>();
    public List<EnumsResponseDto> Region { get; set; } = new List<EnumsResponseDto>();
    public List<BscDto> BSCs { get; set; } = new List<BscDto>();
    public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}
