namespace Application.EmployeeManagement.Dto;

public class UserRegionalProfileDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string StaffId { get; set; }
    public int? GroupId { get; set; }

    public static explicit operator UserRegionalProfileDto(Domain.User.UserProfile userProfile) => new UserRegionalProfileDto
    {
        Name = userProfile.UserFName,
        GroupId = userProfile.GroupId,
        StaffId = userProfile.UserName,
        Email = userProfile.EmailId
    };
}
