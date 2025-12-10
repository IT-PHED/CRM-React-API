namespace Application.Abstractions.Authentication;

public interface IUserContext
{
    string UserId { get; }
    string UserEmail { get; }
}
