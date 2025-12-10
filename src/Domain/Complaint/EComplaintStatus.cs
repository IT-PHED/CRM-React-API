namespace Domain.Complaint;

public enum EComplaintStatus
{
    New,
    Allocated,
    Incident,
    Stage1,
    Stage1_Accepted,
    Stage1_Rejected,
    Stage1_Hold,
    Approved,
    Rejected,
    Closed
}
