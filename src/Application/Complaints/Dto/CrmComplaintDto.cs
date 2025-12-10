namespace Application.Complaints.Dto;

public class CrmComplaintDto
{
    public string Id { get; set; }
    public string ConsumerId { get; set; }
    public string ComplaintSubtypeId { get; set; }
    public string Source { get; set; }
    public string Ticket { get; set; }
    public DateTime? DateGenerated { get; set; }
    public DateTime? DateResolved { get; set; }
    public string Priority { get; set; }
    public string ComplaintTypeId { get; set; }
    public string Remark { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public string MeterNo { get; set; }
    public string TelephoneNo { get; set; }
    public string MaxDemand { get; set; }
    public string Category { get; set; }
    public string Address1 { get; set; }
    public string Address2 { get; set; }
    public string Address3 { get; set; }
    public string Ibc { get; set; }
    public string Bsc { get; set; }
    public string RouteNumber { get; set; }
    public string Email { get; set; }
    public string MobileNo { get; set; }
    public string Type { get; set; }
    public string Dtr { get; set; }
    public string Purpose { get; set; }
    public string MeterDigit { get; set; }
    public string MeterMake { get; set; }
    public int SmsFlag { get; set; }
    public int EmailFlag { get; set; }
    public string RegionId { get; set; }
    public string AdviceType { get; set; }
    public int CdeskId { get; set; }
    public string Sla_Level { get; set; }
    public string Region_Id { get; set; }
    public string Group_Id { get; set; }
    public string Region { get; set; }
}
