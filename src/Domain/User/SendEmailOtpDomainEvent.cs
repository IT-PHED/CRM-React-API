using SharedKernel;

namespace Domain.User;

public sealed record SendEmailOtpDomainEvent(string email, string userName, string subject, string otp, string staffId) : IDomainEvent;
