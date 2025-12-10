namespace SharedKernel;

public interface IAuditableBase
{
    string Id { get; set; }
    DateTime CreatedDate { get; set; }
    string CreatedBy { get; set; }
    DateTime? ModifiedDate { get; set; }
    string? ModifiedBy { get; set; }
    string ConsumerId { get; set; }
}
