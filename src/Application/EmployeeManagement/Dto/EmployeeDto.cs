namespace Application.EmployeeManagement.Dto;

public class EmployeeDto
{
    public string Id { get; set; }
    public string Address { get; set; }
    public string Ibc { get; set; }
    public string Bsc { get; set; }
    public string Code { get; set; }
    public string Email { get; set; }
    public string Designation { get; set; }
    public string Department { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string Name { get; set; }
}

public class EmployeeByIbcBscDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public string MobileNumber { get; set; }
    public string IBc { get; set; }
    public string Bsc { get; set; }
}

public class CrmUserPermissionDto
{
    public int Id { get; set; }
    public string Role_Id { get; set; }
    public string GroupId { get; set; }
    public int View_Perm { get; set; }
    public int Edit_Perm { get; set; }
    public int Reassign_Perm { get; set; }
    public int Resolve_Perm { get; set; }
    public string Designation { get; set; }
}
