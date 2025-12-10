using System.Data;
using Application.Abstractions.Data;
using Application.Abstractions.Repository;
using Dapper;
using Infrastructure.Database;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork;

internal class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _db;
    private IDbContextTransaction? _transaction;
    private bool _disposed;

    public IUserRepository UserRepository { get; init; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        UserRepository = new UserRepository(db);

        //if (_db.Database.GetDbConnection().State != ConnectionState.Open)
        //{
        //    _db.Database.OpenConnection();
        //}
        //_transaction = _db.Database.BeginTransaction();
        EnsureConnectionOpen();
    }

    public IDbConnection Connection => _db.Database.GetDbConnection();

    public IDbTransaction? Transaction => _transaction?.GetDbTransaction();


    private void EnsureConnectionOpen()
    {
        if (_db.Database.GetDbConnection().State != ConnectionState.Open)
        {
            _db.Database.OpenConnection();
        }
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            return;
        }

        EnsureConnectionOpen();
        _transaction = await _db.Database.BeginTransactionAsync();
    }

    public void Save()
    {
        _db.SaveChanges();
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction started");
        }


        try
        {
            await _db.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackAsync()
    {
        if (_transaction == null)
        {
            throw new InvalidOperationException("No transaction started");
        }

        await _transaction.RollbackAsync();
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    public async Task SaveAsync(CancellationToken cancellationToken) => await _db.SaveChangesAsync(cancellationToken);

    public async Task<T> ExecuteStoredProcedureSingleAsync<T>(string storedProcedure, object? param = null)
    {
        return (T)await Connection.QueryAsync<T>(
            storedProcedure,
            param,
            transaction: Transaction,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<T>> ExecuteStoredProcedureAsync<T>(string storedProcedure, object? param = null)
    {
        return await Connection.QueryAsync<T>(
            storedProcedure,
            param,
            transaction: Transaction,
            commandType: CommandType.StoredProcedure);
    }

    public async Task<int> ExecuteStoredProcedureNonQueryAsync(string storedProcedure, object? param = null)
    {
        return await Connection.ExecuteAsync(
            storedProcedure,
            param,
            transaction: Transaction,
            commandType: CommandType.StoredProcedure);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _transaction?.Dispose();
                _db.Dispose();
            }
            _disposed = true;
        }
    }
}
