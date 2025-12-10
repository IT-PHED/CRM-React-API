using System.Data;
using Application.Abstractions.Repository;

namespace Application.Abstractions.Data;

public interface IUnitOfWork
{
    void Save();
    Task SaveAsync(CancellationToken cancellationToken);
    IDbConnection Connection { get; }
    IDbTransaction? Transaction { get; }

    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
    Task<T> ExecuteStoredProcedureSingleAsync<T>(string storedProcedure, object? param = null);
    Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedure, object? param = null);
    Task<int> ExecuteStoredProcedureNonQueryAsync(string storedProcedure, object? param = null);
    IUserRepository UserRepository { get; }

    ///Task<SqlMapper.GridReader> ExecuteStoredProcedureMultipleAsync(string storedProcedure, object? param = null);
}
