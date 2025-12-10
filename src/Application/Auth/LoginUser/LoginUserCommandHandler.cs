using System.Globalization;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Factory;
using Application.Abstractions.Messaging;
using Application.Auth.Dto;
using Domain.ActivityLog;
using Domain.Common;
using Domain.User;
using Microsoft.AspNetCore.Http;
using SharedKernel;

namespace Application.Auth.LoginUser;

internal sealed class LoginUserCommandHandler(IOtpHandler otpHandler,
    IUserService userService,
    IApplicationDbContext context,
    ITripartite tripartite,
    IDateTimeProvider dateTimeProvider,
   IHttpContextAccessor httpContextAccessor,
   ITokenProvider tokenProvider) : ICommandHandler<LoginUserCommand, LoginResponse>
{
    public string? ClientIP
    {
        get
        {
            System.Net.IPAddress? remoteIp = httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;
            return remoteIp?.ToString();
        }
    }

    public async Task<Result<LoginResponse>> Handle(LoginUserCommand command, CancellationToken cancellationToken)
    {

        string UserName = command.Email.Replace(" ", "").Trim();
        string password = command.Password.Trim();

        Domain.User.UserProfile? getUser = await userService.GetUser(UserName);

        if (getUser is null)
        {
            return Result.Failure<LoginResponse>(Domain.User.UserErrors.NotFoundByEmail);
        }

        if (!getUser.isVerified)
        {
            return Result.Failure<LoginResponse>(Domain.Common.CommonErrors.CustomErrorMessage("User not verified"));
        }

        string isPasswordCorrect = tripartite.Encrypt(password);

        if (getUser.Status != "A")
        {
            return Result.Failure<LoginResponse>(Domain.Common.CommonErrors.CustomErrorMessage("Account is inactive, please contact admin"));
        }

        if (isPasswordCorrect != getUser.UserPassword)
        {
            return Result.Failure<LoginResponse>(Domain.Common.CommonErrors.CustomErrorMessage("Username or password is not correct"));
        }

        string lastVerifiedDate = string.Format(CultureInfo.InvariantCulture, "{0:M-d-yyyy hh:mm:ss}", getUser.OTPLastVerifiedTime);
        bool checkLastVerifiedDate = checkLastLoggedInDate(lastVerifiedDate);

        var activityLog = new UserActivityLog
        {
            CheckIn = dateTimeProvider.UtcNow,
            UserId = getUser.UserName,
            Action = EUserLogAction.CHECKIN.ToString(),
            Module = EUserLogModule.LOGIN.ToString(),
            Status = EUserLogStatus.SUCCESS.ToString(),
            ReferenceId = Guid.NewGuid().ToString(),
            PageName = EUserLogPageName.LOGIN.ToString(),
            Desci = $"{getUser.UserFName} Logged In now from CRM from {ClientIP}",
            EmailAddr = getUser.EmailId,
            EmailSent = false,
            AddOn = dateTimeProvider.UtcNow,
        };

        if (!checkLastVerifiedDate)
        {
            string otp = otpHandler.GenerateOtp();

            activityLog = new UserActivityLog
            {
                CheckIn = dateTimeProvider.UtcNow,
                UserId = getUser.UserName,
                Action = EUserLogAction.VERIFY_OTP.ToString(),
                Module = EUserLogModule.LOGIN.ToString(),
                AddOn = dateTimeProvider.UtcNow,
                Status = EUserLogStatus.SUCCESS.ToString(),
                ReferenceId = Guid.NewGuid().ToString(),
                PageName = EUserLogPageName.LOGIN.ToString(),
                Desci = $"OTP sent to {getUser.EmailId} for login verification from {ClientIP}",
                EmailAddr = getUser.EmailId,
                EmailSent = false
            };

            activityLog.Raise(new SendEmailOtpDomainEvent(getUser.EmailId, getUser.UserFName, "One-time-password", otp, getUser.UserName));
        }

        try
        {
            context.UserActivityLog.Add(activityLog);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return Result.Failure<LoginResponse>(Domain.Common.CommonErrors.CustomErrorMessage("Something went wrong"));
        }

        var userObj = (UserProfileDto)getUser;
        string token = tokenProvider.Create(getUser);

        return Result.Success(new LoginResponse { Token = checkLastVerifiedDate ? token : null, IsVerified = checkLastVerifiedDate, UserProfile = userObj });
    }

    private bool checkLastLoggedInDate(string lastLoggedInDate)
    {
        if (string.IsNullOrEmpty(lastLoggedInDate) ||
            !DateTime.TryParse(lastLoggedInDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime lastLoginDate))
        {
            return false;
        }

        TimeSpan timeSinceLastLogin = DateTime.UtcNow.Subtract(lastLoginDate.ToUniversalTime());
        if (timeSinceLastLogin.TotalHours > 24)
        {
            return false;
        }

        return true;
    }
}
