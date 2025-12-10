namespace Web.Api.Endpoints;


/// <summary>
/// Request body for creating a meter-related complaint.
/// </summary>
public class CreateMeterComplaintRequest
{
    /// <summary>
    /// Unique identifier of the consumer (e.g., account number).
    /// </summary>
    public string ConsumerNumber { get; set; } = string.Empty;

    /// <summary>
    /// ID of the complaint type (e.g., "Billing", "Meter").
    /// </summary>
    public string ComplaintTypeId { get; set; } = string.Empty;

    /// <summary>
    /// ID of the complaint subtype (e.g., "High Bill", "Meter Fault").
    /// </summary>
    public string ComplaintSubTypeId { get; set; } = string.Empty;

    /// <summary>
    /// Source of the complaint (e.g., "Web", "Mobile App", "Call Center").
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// Priority level: "Low", "Medium", or "High".
    /// </summary>
    public string Priority { get; set; } = string.Empty;

    /// <summary>
    /// Complainant's email address.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Optional: Staff ID to assign the complaint to (if known).
    /// </summary>
    public string? AssignToStaffId { get; set; }

    /// <summary>
    /// Optional: Staff email to assign the complaint to (alternative to StaffId).
    /// At least one of AssignToStaffId or AssignToEmail must be provided.
    /// </summary>
    public string? AssignToEmail { get; set; }

    /// <summary>
    /// ID of the department handling the complaint.
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// Complainant's mobile number (e.g., "+234123456789").
    /// </summary>
    public string MobileNumber { get; set; } = string.Empty;

    /// <summary>
    /// Optional: Correct meter reading (if provided by complainant).
    /// </summary>
    public int? CorrectMeterReading { get; set; }

    /// <summary>
    /// Optional: Additional remarks about the complaint.
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// Optional: File path or identifier (e.g., uploaded document ID).
    /// </summary>
    public string? File { get; set; }

    /// <summary>
    /// Optional: URL to media (e.g., image/video of issue).
    /// </summary>
    public string? MediaLink { get; set; }
}

public record PagedQuery(
    int? pageNumber = 1,
    int? pageSize = 2000,
    DateTime? dateFrom = null,
    DateTime? dateTo = null);
