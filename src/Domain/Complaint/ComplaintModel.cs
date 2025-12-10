using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Complaint;
public class ComplaintModel
{
    // DB
    public string ComplaintTypeId { get; set; }
    public string ComplaintSubTypeId { get; set; }
    public string Status { get; set; }

    // General
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string ModifiedBy { get; set; }

    // Complaint
    public string Id { get; set; }
    public Consumer.Consumer? Consumer { get; set; }
    public ComplaintSubType? ComplainSubType { get; set; }
    public ConsumerComplaintConstraint Constraint { get; set; }
    public string Ticket { get; set; }
    public DateTime DateGenerated { get; set; }
    public DateTime? DateResolved { get; set; }
    public EComplaintStatus ComplaintStatus { get; set; }
    public string Source { get; set; }
    public string Priority { get; set; }
    public string Remark { get; set; }
    public string Cons_Name { get; set; }
    public string Cons_MeterNo { get; set; }
    public string Cons_TelephoneNo { get; set; }
    public string Con_MaxDemand { get; set; }
    public string Cons_Category { get; set; }
    public string cons_addr1 { get; set; }
    public string cons_addr2 { get; set; }
    public string cons_addr3 { get; set; }
    public string Cons_DivisionCode { get; set; }
    public string Cons_SectionCode { get; set; }
    public string Con_RouteNumber { get; set; }
    public string Con_EmailId { get; set; }
    public string Con_MobileNo { get; set; }
    public string Cons_Type { get; set; }
    public string DTR { get; set; }
    public string Purpose { get; set; }
    public string MeterMake { get; set; }
    public string MeterDigit { get; set; }

    // Constraint
    public string ConstraintId { get; set; }
    public double CorrectMeterReading { get; set; }
    public DateTime MonthFrom { get; set; }
    public DateTime MonthTo { get; set; }
    public string FilePath { get; set; }

    public ComplaintModel()
    {
        ComplaintStatus = EComplaintStatus.New;
    }
}
