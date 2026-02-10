namespace Application.ComplaintResolution.Dto;

public sealed class UpdateComplaintStatusEmailDto
{
    public string StaffName { get; set; }
    public string ComplaintId { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public string ClosedBy { get; set; }
    public string ClosedDate { get; set; }
    public string ClosingRemark { get; set; }
}
