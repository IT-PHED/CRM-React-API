using SharedKernel;

namespace Domain.User;

public class UserProfile : Entity
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string UserPassword { get; set; }
    public int GroupId { get; set; }
    public int EmpId { get; set; }
    public string Access_Auth { get; set; }
    public string AccessCa { get; set; }
    public string Area_Type { get; set; }
    public string Region_Id { get; set; }
    public string Area_Code { get; set; }
    public string PhoneNo { get; set; }
    public string EmailId { get; set; }
    public string Status { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public DateTime? LstPswdUpdatedDate { get; set; }
    public string LastIpAddress { get; set; }
    public string LstLoginDate { get; set; }
    public string LstLogIpAddress { get; set; }
    public string UserFName { get; set; }
    public int smf_role_id { get; set; }
    public string TeamId { get; set; }
    public string DepartmentName { get; set; }
    public string OTPLastVerifiedTime { get; set; }
    public byte[] DEPARTMENT_ID { get; set; }
    public string DepartmentId => DEPARTMENT_ID == null
        ? null
        : Convert.ToHexString(DEPARTMENT_ID);
    public string is_verified { get; set; }
    public bool isVerified
    {
        get => is_verified == "1"; set => is_verified = value ? "1" : "0";
    }
}
