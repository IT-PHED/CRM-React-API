using Domain.User;

namespace Application.Abstractions.Factory;

public interface IUserService
{
    Task<UserProfile> GetUser(string emailOrStaffId);
    Task updateOTP(string email, string staffId, string otp);
    Task verifyOtp(string email, string staffId, string otp);
}
