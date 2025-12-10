using SharedKernel;

namespace Domain.ActivityLog;

public sealed class UserActivityLog : Entity
{
    public string UserId { get; set; }
    public string Action { get; set; }
    public string Status { get; set; }
    public string Module { get; set; }
    public string Desci { get; set; }

    /// OPTIONAL FIELDS
    public string ConsumerNo { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime? CheckIn { get; set; }
    public DateTime? CheckOut { get; set; }
    public string ReferenceId { get; set; }
    public string PageName { get; set; }
    public DateTime? AddOn { get; set; }
    public bool? EmailSent { get; set; }
    public string EmailAddr { get; set; }
}
