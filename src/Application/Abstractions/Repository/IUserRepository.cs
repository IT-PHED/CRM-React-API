using Domain.User;

namespace Application.Abstractions.Repository;
public interface IUserRepository : IRepository<UserProfile>
{
    void Update(UserProfile userProfile);
}
