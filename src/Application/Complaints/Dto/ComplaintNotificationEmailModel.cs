namespace Application.Complaints.Dto;

public class ComplaintNotificationEmailModel
{
    public string ComplaintId { get; set; } = string.Empty;
    public string AssigneeName { get; set; } = string.Empty;
    public DateTime SubmissionDate { get; set; }
    public string Priority { get; set; } = "High";
    public string ComplaintType { get; set; } = string.Empty;
    public string ComplainantName { get; set; } = string.Empty;
    public string ComplainantEmail { get; set; } = string.Empty;
    public string ComplainantPhone { get; set; } = string.Empty;
    public string SubmittedBy { get; set; } = string.Empty;
    public string ComplaintDescription { get; set; } = string.Empty;
    public string? Url { get; set; } = string.Empty;
}
