using Domain.User;

namespace Application.Auth.Dto;

public class UserProfileDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public int GroupId { get; set; }
    public string? Area_Type { get; set; }
    public string? Region_Id { get; set; }
    public string? PhoneNo { get; set; }
    public string? EmailId { get; set; }
    public string Status { get; set; }
    public string? CreatedBy { get; set; }
    public string? CreatedDate { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? LstPswdUpdatedDate { get; set; }
    public string? LastIpAddress { get; set; }
    public string? LstLoginDate { get; set; }
    public string? UserFName { get; set; }
    public int? smf_role_id { get; set; }
    public string? TeamId { get; set; }
    public string? DepartmentName { get; set; }
    public string? OTPLastVerifiedTime { get; set; }
    public string? DepartmentId { get; set; }
    public bool isVerified
    {
        get; set;
    }

    public static explicit operator UserProfileDto(Domain.User.UserProfile userProfile) => new UserProfileDto
    {
        UserId = userProfile.UserId,
        UserName = userProfile.UserName,
        GroupId = userProfile.GroupId,
        Area_Type = userProfile.Area_Type,
        Region_Id = userProfile.Region_Id,
        PhoneNo = userProfile.PhoneNo,
        EmailId = userProfile.EmailId,
        Status = userProfile.Status,
        CreatedBy = userProfile.CreatedBy,
        CreatedDate = userProfile.CreatedDate,
        ModifiedBy = userProfile.ModifiedBy,
        ModifiedDate = userProfile.ModifiedDate,
        LstPswdUpdatedDate = userProfile.LstPswdUpdatedDate,
        LastIpAddress = userProfile.LastIpAddress,
        LstLoginDate = userProfile.LstLoginDate,
        UserFName = userProfile.UserFName,
        smf_role_id = userProfile.smf_role_id,
        TeamId = userProfile.TeamId,
        DepartmentName = userProfile.DepartmentName,
        OTPLastVerifiedTime = userProfile.OTPLastVerifiedTime,
        DepartmentId = userProfile.DepartmentId,
        isVerified = userProfile.isVerified
    };
}
