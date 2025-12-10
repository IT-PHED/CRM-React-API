using Application.Abstractions.Factory;
using Application.Auth.Dto;
using Domain.User;
using SharedKernel;

namespace Application.Auth.LoginUser;

internal sealed class SendEmailOtpDomainEventHandler(
    IRazorViewToString razorViewToString,
    IEmailService emailService,
    IUserService userService) : IDomainEventHandler<SendEmailOtpDomainEvent>
{
    public async Task Handle(SendEmailOtpDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        string email = domainEvent.email;
        string userName = domainEvent.userName;
        string subject = domainEvent.subject;
        string otp = domainEvent.otp;
        string staffId = domainEvent.staffId;

        var emailModel = new OtpEmailModel
        {
            Email = email,
            Otp = otp,
        };

        await userService.updateOTP(email, staffId, otp);

        string emailBody = await razorViewToString.RenderViewToStringAsync("/Views/RazorEmailTemplate/OTPEmailTemplate.cshtml", emailModel);

        await emailService.SendCommonEmail(userName, email, emailBody, new List<string> { }, subject);
    }
}
