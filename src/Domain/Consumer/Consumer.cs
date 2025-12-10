using Domain.Complaint;
using SharedKernel;

namespace Domain.Consumer;

public class Consumer : Entity
{
    private string cons_acc;
    public string Id { get; set; }
    public string Cons_Acc
    {
        get => cons_acc;
        protected set
        {
            cons_acc = value;
            Id = value;
        }
    }
    public string Cons_Name { get; protected set; }
    public string Con_MobileNo { get; protected set; }
    public string Con_Village { get; protected set; }
    public string Con_RouteNumber { get; protected set; }  //METER NO
    public string Con_MaxDemand { get; protected set; }
    public string cons_addr1 { get; protected set; }
    public string cons_addr2 { get; protected set; }
    public string cons_addr3 { get; protected set; }
    public string Cons_Type { get; protected set; }
    public string Cons_Category { get; protected set; }
    public string Cons_DivisionCode { get; protected set; }
    public string Cons_SectionCode { get; protected set; }
    public string Con_ConsumerStatus { get; protected set; }
    public string Con_Street { get; protected set; }  //ADDRESS
    public string Con_City { get; protected set; }    // Landmark
    public string Con_EmailId { get; protected set; }
    public string Cons_TelephoneNo { get; protected set; }
    public string Con_PoleNumber { get; protected set; }
    public string Con_FeederCode { get; protected set; }
    public string Con_Transformer_Owner { get; protected set; }
    public string Con_PreviousMeterReadingDate { get; protected set; }
    public string Con_PreviousMeterReading { get; protected set; }
    public string Cons_MeterNo { get; protected set; }
    public string Purpose { get; protected set; }
    public string Con_MeterMake { get; protected set; }

    public ICollection<Complaint.ConsumerComplaint> Complaints { get; set; } = new List<Complaint.ConsumerComplaint>();

    public void AddComplaint(Domain.Complaint.ComplaintModel complaint)
    {
        var consumerComplaint = new ConsumerComplaint(complaint);
        Complaints.Add(consumerComplaint);
    }
}
