using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Product.Management.Api.DatabaseContext;
using System;
using System.Text;

namespace Product.Management.Api.UnitofWork;

public class UnitOfwork(ProductManagementContext context) : IUnitOfwork
{
    private readonly ProductManagementContext _context = context;
    private bool _disposed = false;
    private IDbContextTransaction _objTran;
    private string _errorMessage = string.Empty;

    public DbContext Context => _context;

    public void CreateTransaction()
    {
        //It will Begin the transaction on the underlying store connection
        _objTran = Context.Database.BeginTransaction();
    }

    public void Commit()
    {
        //Commits the underlying store transaction
        _objTran.Commit();
    }

    public void Rollback()
    {
        //Rolls back the underlying store transaction
        _objTran.Rollback();
        //The Dispose Method will clean up this transaction object and ensures Entity Framework
        //is no longer using that transaction.
        _objTran.Dispose();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException dbu)
        {
            _errorMessage = HandleUnitOfWorkException(dbu);
            throw new Exception(_errorMessage, dbu);
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
            if (disposing)
                _context.Dispose();

        _disposed = true;
    }

    private string HandleUnitOfWorkException(DbUpdateException dbu)
    {
        var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");
        try
        {
            foreach (var result in dbu.Entries)
            {
                builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
            }
        }
        catch (Exception e)
        {
            builder.Append("Error parsing DbUpdateException: " + e.ToString());
        }
        return _errorMessage = builder.ToString();
    }
}
