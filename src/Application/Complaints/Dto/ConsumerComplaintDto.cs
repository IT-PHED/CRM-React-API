using Org.BouncyCastle.Bcpg.OpenPgp;

namespace Application.Complaints.Dto;

public class ConsumerComplaintDto
{
    public string Id { get; set; }
    public string Ticket { get; set; }
    public DateTime DateGenerated { get; set; }
    public DateTime? DateResolved { get; set; }
    public bool IsResolved { get; set; }
    public string Status { get; set; }
    public string ConsumerId { get; set; }
    public string ConsumerNumber { get; set; }
    public string ConsumerName { get; set; }
    public string? ConsumerAddress { get; set; }
    public string? ConsumerCategory { get; set; }
    public string? ConsumerPhoneNumber { get; set; }
    public string ComplaintTypeId { get; set; }
    public string? ComplaintType { get; set; }
    public string ComplaintSubTypeId { get; set; }
    public string? ComplaintSubType { get; set; }
    public string? Remark { get; set; }
    public string? MediaURL { get; set; }
    public int? CorrectMeterReading { get; set; }
    public string Source { get; set; }
    public string SlaLevel { get; set; }
    public string? Email { get; set; }
    public string? MobileNo { get; set; }
    public string? TelephoneNo { get; set; }

    public string? MeterNo { get; set; }
    public string? MaxDemand { get; set; }
    public string? RouteNumber { get; set; }
    public string? Ibc { get; set; }
    public string? Bsc { get; set; }
    public string? Address1 { get; set; }
    public string? Address2 { get; set; }
    public string? Address3 { get; set; }
    public string? AssignedTo { get; set; }
    public string? GroupId { get; set; }
    public string? ResolvedBy { get; set; }
    public string? Feedback { get; set; }
    public string? ClosedBy { get; set; }
    public string? ClosedByRemark { get; set; }
    public DateTime? ClosedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
