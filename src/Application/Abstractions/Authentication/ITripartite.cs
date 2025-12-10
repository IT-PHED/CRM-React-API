namespace Application.Abstractions.Authentication;

public interface ITripartite
{
    string Encrypt(string password);
}
