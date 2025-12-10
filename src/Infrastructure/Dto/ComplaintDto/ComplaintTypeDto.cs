using Domain.Common;

namespace Infrastructure.Dto.ComplaintDto;

public class ComplaintTypeDto : AuditableBase
{
    public string Name { get; set; }
    public int Code { get; set; }
    public new string CreatedBy { get; set; }
    public List<ComplaintSubTypeDto> SubTypes { get; set; } = new List<ComplaintSubTypeDto>();
    public int GroupId { get; set; }
    public string DepartmentId { get; set; }
}
