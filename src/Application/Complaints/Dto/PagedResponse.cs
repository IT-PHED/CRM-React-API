namespace Application.Complaints.Dto;

public class PagedResponse<T>
{
    public IEnumerable<T> Data { get; set; }
    public int? TotalItems { get; set; } = 0;
    public int? PageSize { get; set; } = 0;
    public int? PageNumber { get; set; } = 0;
}
