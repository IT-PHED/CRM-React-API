namespace Application.EscalationMatrixResolution.Dto;

public class EscalationMatrixResolutionDto
{
    public string CONSUMERNO { get; set; }
    public string COMPLAINTTYPEID { get; set; }
    public string COMPLAINTSUBTYPEID { get; set; }
    public string REMARK { get; set; }
    public string TICKET { get; set; }
    public string SOURCE { get; set; }
    public string CONS_PHONE_NO { get; set; }
    public string ADDRESS1 { get; set; }
    public string ID { get; set; }
    public int DESIGNATIONID { get; set; }
    public int SLA_LEVEL { get; set; }
    public int REGION_ID { get; set; }
    public string ROUTENUMBER { get; set; }
    public DateTime CREATEDDATE { get; set; }
    public string FEEDER11NAME { get; set; }
    public string FEEDER33NAME { get; set; }
    public string REGIONAL_OFFICES { get; set; }
    public double TIME_DIFF_MINUTES { get; set; }
    public int PERIOD { get; set; }
    public string PERIODTYPE { get; set; }
    public int diff { get; set; }
    public string name { get; set; }
    public string MOBILENUMBER { get; set; }
    public string EMAIL { get; set; }
    public string officer { get; set; }
    public int complaint_region_with_space { get; set; }
    public int complaint_region_no_space { get; set; }
    public int feeder_region { get; set; }
    public int emp_region { get; set; }
    public string emp_area_region { get; set; }
    public string COMPLAINTTYPE { get; set; }
    public string COMPLAINTSUBTYPE { get; set; }
    public string levelname { get; set; }
    public string RESENT_MAIL { get; set; }
    public string ESCALATION_REMARK { get; set; }
    public string escalation_id { get; set; }
    public DateTime ESCALATION_CREATEDDATE { get; set; }
    public string CONFIGID { get; set; }
    public string EMPID { get; set; }
    public int ShouldCloseTicket { get; set; }
    public DateTime? DATERESOLVED { get; set; }
}
