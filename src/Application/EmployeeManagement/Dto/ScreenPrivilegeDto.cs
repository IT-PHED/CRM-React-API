namespace Application.EmployeeManagement.Dto;

public class ScreenPrivilegeDto
{
    public int Privilegeid { get; set; }
    public string ScreenName { get; set; }
    public string ScreenTitle { get; set; }
    public string PageLinks { get; set; }
    public string ParentKey { get; set; }
    public string IsActive { get; set; }
    public string P_VIEW { get; set; }
    public string P_ADD { get; set; }
    public string P_APPROVE { get; set; }
    public string P_DELETE { get; set; }
}
