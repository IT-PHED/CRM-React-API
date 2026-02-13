namespace Web.Api.Endpoints;




/// <summary>
/// Request body for creating a meter-related complaint.
/// </summary>
public class CreateRequestWithoutAccount
{
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
    /// ID of the department handling the complaint.
    /// </summary>
    public string DepartmentId { get; set; } = string.Empty;

    /// <summary>
    /// Complainant's mobile number (e.g., "+234123456789").
    /// </summary>
    public string MobileNumber { get; set; } = string.Empty;

    /// <summary>
    /// Optional: Additional remarks about the complaint.
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// Optional: File path or identifier (e.g., uploaded document ID).
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Optional: URL to media (e.g., image/video of issue).
    /// </summary>
    public string Address { get; set; }
    public string RegionId { get; set; }
    public string CustomerName { get; set; }
}


