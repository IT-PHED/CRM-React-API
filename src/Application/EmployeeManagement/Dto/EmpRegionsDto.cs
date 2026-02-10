namespace Application.EmployeeManagement.Dto;

public class EmpRegionsDto
{
    public string ID { get; set; }
    public string REGIONAL_OFFICES { get; set; }
}

public class CrmRoleMenu
{
    public string Id { get; set; }
    public string RoleId { get; set; }
    public string GroupId { get; set; }
    public bool ViewPrem { get; set; }
    public bool EditPrem { get; set; }
    public bool ReassignPrem { get; set; }
    public bool ResolvePrem { get; set; }
    public string Designation { get; set; } = string.Empty;
}

public class SmsInfoDto
{
    public string TEMPLATE_ID { get; set; }
    public string TEMPLATE_TEXT { get; set; }
    public string TEMPLATE_TITLE { get; set; }
    public string ACTIVE { get; set; }
    public string TEMPLATE_DATE { get; set; }
    public string TEMPLATE_DELIVERY_PERIOD { get; set; }

}
