using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Domain.Complaint;

public class Complaint : Entity, IAuditableBase
{
    [Column("ID")]
    [MaxLength(60)]
    public string Id { get; set; } = string.Empty;

    [Column("CONSUMERID")]
    [MaxLength(60)]
    public string ConsumerId { get; set; } = string.Empty;

    [Column("COMPLAINTSUBTYPEID")]
    [MaxLength(60)]
    public string ComplaintSubTypeId { get; set; } = string.Empty;

    [Column("SOURCE")]
    [MaxLength(200)]
    public string Source { get; set; } = string.Empty;

    [Column("TICKET")]
    [MaxLength(60)]
    public string Ticket { get; set; } = string.Empty;

    [Column("DATEGENERATED")]
    public DateTime? DateGenerated { get; set; }

    [Column("DATERESOLVED")]
    public DateTime? DateResolved { get; set; }

    [Column("PRIORITY")]
    [MaxLength(60)]
    public string Priority { get; set; } = string.Empty;

    [Column("COMPLAINTTYPEID")]
    [MaxLength(60)]
    public string ComplaintTypeId { get; set; } = default!;

    [Column("REMARK")]
    [MaxLength(300)]
    public string Remark { get; set; } = string.Empty;

    [Column("CREATEDBY")]
    [MaxLength(20)]
    public string CreatedBy { get; set; } = string.Empty;

    [Column("CREATEDDATE")]
    public DateTime CreatedDate { get; set; }

    [Column("MODIFIEDBY")]
    [MaxLength(20)]
    public string? ModifiedBy { get; set; } = string.Empty;

    [Column("MODIFIEDDATE")]
    public DateTime? ModifiedDate { get; set; }

    [Column("STATUS")]
    [MaxLength(20)]
    public string Status { get; set; } = string.Empty;

    [Column("NAME")]
    [MaxLength(60)]
    public string Name { get; set; } = string.Empty;

    [Column("METERNO")]
    [MaxLength(60)]
    public string MeterNo { get; set; } = string.Empty;

    [Column("TELEPHONENO")]
    [MaxLength(60)]
    public string TelephoneNo { get; set; } = string.Empty;

    [Column("MAXDEMAND")]
    [MaxLength(60)]
    public string MaxDemand { get; set; } = string.Empty;

    [Column("CATEGORY")]
    [MaxLength(60)]
    public string Category { get; set; } = string.Empty;

    [Column("ADDRESS1")]
    [MaxLength(300)]
    public string Address1 { get; set; } = string.Empty;

    [Column("ADDRESS2")]
    [MaxLength(300)]
    public string Address2 { get; set; } = string.Empty;

    [Column("ADDRESS3")]
    [MaxLength(300)]
    public string Address3 { get; set; } = string.Empty;

    [Column("IBC")]
    [MaxLength(60)]
    public string Ibc { get; set; } = string.Empty;

    [Column("BSC")]
    [MaxLength(60)]
    public string Bsc { get; set; } = string.Empty;

    [Column("ROUTENUMBER")]
    [MaxLength(60)]
    public string RouteNumber { get; set; } = string.Empty;

    [Column("EMAIL")]
    [MaxLength(60)]
    public string Email { get; set; } = string.Empty;

    [Column("MOBILENO")]
    [MaxLength(60)]
    public string MobileNo { get; set; } = string.Empty;

    [Column("TYPE")]
    [MaxLength(60)]
    public string Type { get; set; } = string.Empty;

    [Column("DTR")]
    [MaxLength(60)]
    public string Dtr { get; set; } = string.Empty;

    [Column("PURPOSE")]
    [MaxLength(200)]
    public string Purpose { get; set; } = string.Empty;

    [Column("METERDIGIT")]
    [MaxLength(20)]
    public string MeterDigit { get; set; } = string.Empty;

    [Column("METERMAKE")]
    [MaxLength(20)]
    public string MeterMake { get; set; } = string.Empty;

    [Column("SMSFLAG")]
    public int? SmsFlag { get; set; }

    [Column("EMAIL_FLAG")]
    public int? EmailFlag { get; set; }

    [Column("REGIONID")]
    [MaxLength(20)]
    public string RegionId { get; set; } = string.Empty;

    [Column("ADVICETYPE")]
    [MaxLength(20)]
    public string AdviceType { get; set; } = string.Empty;

    [Column("CDESKID")]
    public int? CDeskId { get; set; }

    [Column("SLA_LEVEL")]
    [MaxLength(4)]
    public string SlaLevel { get; set; } = string.Empty;

    [Column("REGION_ID")]
    [MaxLength(20)]
    public string Region_Id { get; set; } = string.Empty;

    [Column("GROUP_ID")]
    public int? GroupId { get; set; }

    [Column("ASSIGNEDTO")]
    [MaxLength(50)]
    public string AssignedTo { get; set; } = string.Empty;

    [Column("FEEDBACK")]
    [MaxLength(4000)]
    public string? Feedback { get; set; }

    [Column("DEPARTMENTID")]
    [MaxLength(50)]
    public string DepartmentId { get; set; } = string.Empty;

    [Column("RESOLVEDBY")]
    [MaxLength(50)]
    public string ResolvedBy { get; set; } = string.Empty;

    [Column("MEDIAURL")]
    [MaxLength(4000)]
    public string? MediaUrl { get; set; }

    [Column("CLOSEDBY")]
    [MaxLength(60)]
    public string ClosedBy { get; set; } = string.Empty;

    [Column("CLOSEDBY_REMARK")]
    [MaxLength(4000)]
    public string? ClosedByRemark { get; set; }

    [Column("CLOSED_DATE")]
    public DateTime? ClosedDate { get; set; }
}

