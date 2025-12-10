namespace Application.Configuration.Dto;

public class ComplaintTypeResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public string CreatedBy { get; set; }
    public List<ComplaintSubTypeResponseDto>? SubTypes { get; set; } = new();
    public int GroupId { get; set; }
    public string DepartmentId { get; set; }
    public DateTime CreatedDate { get; set; }
}
