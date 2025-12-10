using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SharedKernel;

namespace Domain.Complaint;

public class ComplaintConstraint : Entity
{
    [Key]
    [Column("ID")]
    public string Id { get; set; }

    [Column("COMPLAINTID")]
    [StringLength(60)]
    public string ComplaintId { get; set; }

    [Column("CORRECTMETERREADING")]
    public decimal? CorrectMeterReading { get; set; }

    [Column("CREATEDBY")]
    [StringLength(60)]
    public string CreatedBy { get; set; }

    [Column("CREATEDDATE")]
    public DateTime? CreatedDate { get; set; }

    [Column("MODIFIEDBY")]
    [StringLength(60)]
    public string ModifiedBy { get; set; }

    [Column("MODIFIEDDATE")]
    public DateTime? ModifiedDate { get; set; }

    [Column("MONTHFROM")]
    public DateTime? MonthFrom { get; set; }

    [Column("MONTHTO")]
    public DateTime? MonthTo { get; set; }

    [Column("FILEPATH")]
    [StringLength(300)]
    public string FilePath { get; set; }
}
