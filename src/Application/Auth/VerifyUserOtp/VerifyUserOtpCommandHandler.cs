using Application.Abstractions.Authentication;
using Application.Abstractions.Factory;
using Application.Abstractions.Messaging;
using Application.Auth.Dto;
using SharedKernel;

namespace Application.Auth.VerifyUserOtp;

internal sealed class VerifyUserOtpCommandHandler(IUserService userService, ITokenProvider tokenProvider) : ICommandHandler<VerifyUserOtpCommand, VerifyOtpResponse>
{
    public async Task<Result<VerifyOtpResponse>> Handle(VerifyUserOtpCommand command, CancellationToken cancellationToken)
    {
        Domain.User.UserProfile? getUser = await userService.GetUser(command.StaffId);

        if (getUser is null)
        {
            return Result.Failure<VerifyOtpResponse>(Domain.User.UserErrors.NotFoundByEmail);
        }

        await userService.verifyOtp(command.Email, command.StaffId, command.Otp);

        string token = tokenProvider.Create(getUser);

        return Result.Success(new VerifyOtpResponse
        {
            IsVerified = true,
            Token = token,
            UserProfile = (UserProfileDto)getUser
        });
    }
}
