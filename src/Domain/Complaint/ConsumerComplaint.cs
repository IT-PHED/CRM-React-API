using Domain.Consumer;
using SharedKernel;

namespace Domain.Complaint;

public class ConsumerComplaint : Entity, IAuditableBase
{
    public Consumer.Consumer? Consumer { get; protected set; }
    public ComplaintSubType? ComplainSubType { get; protected set; }
    public ConsumerComplaintConstraint Constraint { get; protected set; }
    public string Ticket { get; protected set; }
    public DateTime? DateGenerated { get; protected set; }
    public DateTime? DateResolved { get; protected set; }
    public EComplaintStatus Status { get; protected set; }
    public string Source { get; protected set; }
    public string Priority { get; protected set; }
    public string Remark { get; protected set; }
    public string Cons_Name { get; protected set; }
    public string Cons_MeterNo { get; protected set; }
    public string Cons_TelephoneNo { get; protected set; }
    public string Con_MaxDemand { get; protected set; }
    public string cons_addr1 { get; protected set; }
    public string cons_addr2 { get; protected set; }
    public string cons_addr3 { get; protected set; }
    public string Cons_DivisionCode { get; protected set; }
    public string Cons_SectionCode { get; protected set; }
    public string Con_RouteNumber { get; protected set; }
    public string Con_EmailId { get; protected set; }
    public string Con_MobileNo { get; protected set; }
    public string Cons_Type { get; protected set; }
    public string DTR { get; protected set; }
    public string Cons_Category { get; protected set; }
    public string Purpose { get; protected set; }
    public string MeterMake { get; protected set; }
    public string MeterDigit { get; protected set; }
    public string ResolvedBy { get; protected set; }
    public string Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public string ConsumerId { get; set; }

    internal ConsumerComplaint(Domain.Complaint.ComplaintModel complaint) : this(complaint.Id)
    {
        Consumer = complaint.Consumer;
        ComplainSubType = complaint.ComplainSubType;
        DateGenerated = complaint.DateGenerated;
        DateResolved = complaint.DateResolved;
        Status = complaint.ComplaintStatus;
        Ticket = complaint.Ticket;
        Source = complaint.Source;
        Priority = complaint.Priority;
        Remark = complaint.Remark;
        Cons_Name = complaint.Cons_Name;
        Cons_MeterNo = complaint.Cons_MeterNo;
        Cons_TelephoneNo = complaint.Cons_TelephoneNo;
        Con_MaxDemand = complaint.Con_MaxDemand;
        Cons_Category = complaint.Cons_Category;
        cons_addr1 = complaint.cons_addr1;
        cons_addr2 = complaint.cons_addr2;
        cons_addr3 = complaint.cons_addr3;
        Cons_DivisionCode = complaint.Cons_DivisionCode;
        Cons_SectionCode = complaint.Cons_SectionCode;
        Con_RouteNumber = complaint.Con_RouteNumber;
        Con_EmailId = complaint.Con_EmailId;
        Con_MobileNo = complaint.Con_MobileNo;
        Cons_Type = complaint.Cons_Type;
        DTR = complaint.DTR;
        Purpose = complaint.Purpose;
        MeterMake = complaint.MeterMake;
        MeterDigit = complaint.MeterDigit;
        Id = complaint.Id;
        CreatedBy = complaint.CreatedBy;
        CreatedDate = complaint.CreatedDate;
        ModifiedBy = complaint.ModifiedBy;
        ModifiedDate = complaint.ModifiedDate;
        Constraint = new ConsumerComplaintConstraint(this,
                                                     complaint.CorrectMeterReading,
                                                     complaint.MonthFrom,
                                                     complaint.MonthTo,
                                                     complaint.FilePath,
                                                     complaint.CreatedBy,
                                                     complaint.CreatedDate,
                                                     complaint.ModifiedBy,
                                                     complaint.ModifiedDate,
                                                     complaint.ConstraintId);
    }

    public ConsumerComplaint(string id)
    {
        Id = id;
    }
}
