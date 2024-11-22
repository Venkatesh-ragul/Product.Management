using Microsoft.EntityFrameworkCore;

namespace Product.Management.Api.UnitofWork;

public interface IUnitOfwork : IDisposable
{
    DbContext Context { get; }

    //Start the database Transaction
    void CreateTransaction();

    //Commit the database Transaction
    void Commit();

    //Rollback the database Transaction
    void Rollback();

    //DbContext Class SaveChanges method
    public Task SaveChangesAsync();
}
