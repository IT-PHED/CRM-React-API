namespace Application.Abstractions.Factory;

public interface ISmsService
{
    Task SendSms(string Message, string PhoneNumber, CancellationToken cancellationToken = default);
}
