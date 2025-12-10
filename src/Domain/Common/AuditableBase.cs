using SharedKernel;

namespace Domain.Common;

public abstract class AuditableBase : IAuditableBase
{
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public string ConsumerId { get; set; }

    protected AuditableBase()
    {
        Id = Guid.NewGuid().ToString();
        CreatedDate = DateTime.UtcNow;
    }

    protected AuditableBase(string id) : this()
    {
        Id = !string.IsNullOrWhiteSpace(id) ? id : Guid.NewGuid().ToString();
    }
}
