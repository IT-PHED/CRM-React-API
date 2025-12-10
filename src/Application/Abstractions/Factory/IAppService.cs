namespace Application.Abstractions.Factory;

public interface IAppService
{
    string GetBaseLink();
    string ConvertEmailToName(string email);
}
