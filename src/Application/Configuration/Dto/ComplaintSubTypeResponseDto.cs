namespace Application.Configuration.Dto;

public class ComplaintSubTypeResponseDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Code { get; set; }
    public string? ComplaintId { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
}
