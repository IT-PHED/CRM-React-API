namespace Application.ComplaintResolution.Dto;

public sealed class UpdateComplaintDto
{
    public string Id { get; set; }
    public string ConsumerId { get; set; }
    public string ComplaintTypeId { get; set; }
    public string ComplaintSubTypeId { get; set; }
    public string Ticket { get; set; }
    public DateTime DateGenerated { get; set; }
    public DateTime? DateResolved { get; set; }
    public string Status { get; set; }
    public string Source { get; set; }
    public string Priority { get; set; }
    public string Remark { get; set; }
    public string Con_EmailId { get; set; }
    public string Con_MobileNo { get; set; }
    public string cons_addr1 { get; set; }
    public string cons_addr2 { get; set; }
    public string cons_addr3 { get; set; }
    public string Cons_Name { get; set; }
    public string Purpose { get; set; }
    public string MeterMake { get; set; }
    public string MeterDigit { get; set; }
    public string Cons_Category { get; set; }
    public string ResolvedBy { get; set; }
    public string Feedback { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
}
