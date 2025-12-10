using Domain.ActivityLog;
using Domain.Complaint;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    DbSet<UserProfile> Users { get; }
    DbSet<UserActivityLog> UserActivityLog { get; }
    DbSet<Complaint> Complaints { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
