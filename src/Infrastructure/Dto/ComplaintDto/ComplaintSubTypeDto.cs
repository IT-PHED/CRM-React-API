using Domain.Common;

namespace Infrastructure.Dto.ComplaintDto;

public class ComplaintSubTypeDto : AuditableBase
{
    public string Name { get; set; }
    public int Code { get; set; }
    public string ComplaintId { get; set; }
}
