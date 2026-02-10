namespace Application.Dashboard.Dto;

public class ComplaintMonthlyStat
{
    public int Total { get; set; }
    public int Unresolved { get; set; }
    public int Resolved { get; set; }
    public int Overdue { get; set; }
    public int Today { get; set; }
}
