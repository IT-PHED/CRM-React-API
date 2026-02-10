namespace Application.ComplaintResolution.Dto;

public class CustomerCloseOutEmailViewModelDto
{
    public string CustomerName { get; set; } = default!;
    public string Ticket { get; set; } = default!;
    public string ComplaintDescription { get; set; } = default!;
    public DateTime ClosedDate { get; set; }
}
