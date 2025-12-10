namespace Application.Auth.Dto;

public class LoginResponse
{
    public bool IsVerified { get; set; }
    public string? Token { get; set; }
    public UserProfileDto UserProfile { get; set; }
}


public class VerifyOtpResponse
{
    public bool IsVerified { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public UserProfileDto UserProfile { get; set; }
}
