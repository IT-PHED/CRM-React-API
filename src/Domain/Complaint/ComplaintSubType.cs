using Domain.Common;

namespace Domain.Complaint;

public class ComplaintSubType : AuditableBase
{
    public string Name { get; protected set; }
    public int Code { get; protected set; }
    public ComplaintType ComplainType { get; protected set; }

    public ComplaintSubType() { }

    internal ComplaintSubType(ComplaintType complaintType, string name, int code, string createdBy, DateTime createdDate, string? modifiedBy = null, DateTime? modifiedDate = null, string? id = null)
    {
        Id = id ?? Guid.NewGuid().ToString();
        Name = name;
        Code = code;
        ComplainType = complaintType;
        CreatedBy = createdBy;
        CreatedDate = createdDate;
        ModifiedBy = modifiedBy ?? "";
        ModifiedDate = modifiedDate;
    }
}
