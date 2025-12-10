namespace Application.Complaints.GetComplaintById;

public class GetComplaintByIdQueryResponse
{
    public string Id { get; set; } = string.Empty;
    public string Complainttypeid { get; set; } = string.Empty;
    public string Complaintsubtypeid { get; set; } = string.Empty;
    public string Consumerid { get; set; } = string.Empty;
    public DateTime? Dategenerated { get; set; }
    public DateTime? Dateresolved { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Priority { get; set; } = string.Empty;
    public string Remark { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Ticket { get; set; } = string.Empty;

    public string CONS_ACC { get; set; } = string.Empty;          // cc.consumerid as CONS_ACC
    public string CONS_NAME { get; set; } = string.Empty;         // cc.name as CONS_NAME
    public string CONS_METERNO { get; set; } = string.Empty;      // cc.meterno as CONS_METERNO
    public string CONS_TELEPHONENO { get; set; } = string.Empty;  // cc.telephoneno as CONS_TELEPHONENO
    public string Con_MobileNo { get; set; } = string.Empty;      // cc.telephoneno as Con_MobileNo
    public string CON_MAXDEMAND { get; set; } = string.Empty;     // cc.maxdemand as CON_MAXDEMAND
    public string Cons_Category { get; set; } = string.Empty;     // cc.category as Cons_Category
    public string Cons_addr1 { get; set; } = string.Empty;        // cc.address1 as cons_addr1
    public string Cons_addr2 { get; set; } = string.Empty;        // cc.address2 as cons_addr2
    public string Cons_addr3 { get; set; } = string.Empty;        // cc.address3 as cons_addr3
    public string Cons_Type { get; set; } = string.Empty;         // cc.type as Cons_Type
    public string Con_emailid { get; set; } = string.Empty;       // cc.email as con_emailid
    public string Cons_DivisionCode { get; set; } = string.Empty; // cc.ibc as Cons_DivisionCode
    public string Cons_SectionCode { get; set; } = string.Empty;  // cc.bsc as Cons_SectionCode
    public string Dtr { get; set; } = string.Empty;               // cc.dtr
    public string Purpose { get; set; } = string.Empty;           // cc.purpose
    public string Meterdigit { get; set; } = string.Empty;        // cc.meterdigit
    public string Metermake { get; set; } = string.Empty;         // cc.metermake
    public decimal? Correctmeterreading { get; set; }             // ccc.correctmeterreading
    public string? Filepath { get; set; }                         // ccc.filepath
    public string? Monthfrom { get; set; }
    public string? Monthto { get; set; }
}
