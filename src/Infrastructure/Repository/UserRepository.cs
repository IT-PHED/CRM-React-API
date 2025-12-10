using Application.Abstractions.Data;
using Application.Abstractions.Repository;
using Domain.User;
using Infrastructure.Database;

namespace Infrastructure.Repository;

internal sealed class UserRepository : Repository<UserProfile>, IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(UserProfile userProfile)
    {
        _db.Users.Update(userProfile);
    }
}
