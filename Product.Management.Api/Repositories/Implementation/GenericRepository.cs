using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product.Management.Api.DatabaseContext;
using Product.Management.Api.Repositories.Interfaces;
using Product.Management.Api.Models.Domain;
using Product.Management.Api.UnitofWork;
using Product.Management.Api.Models.DTO;
using System.Text;

namespace Product.Management.Api.Repositories.Implementation;

public class GenericRepository<T> : ControllerBase, IGenericRepository<T> where T : class
{
    private readonly IUnitOfwork _unitOfWork;
    protected DbSet<T> _dbSet;

    private bool _isDisposed;
    private string _errorMessage = string.Empty;

    public GenericRepository(IUnitOfwork unitOfwork)
    {
        _isDisposed = false;
        _unitOfWork = unitOfwork;
        _dbSet = _unitOfWork.Context.Set<T>();
    }

    public async Task<ActionResult<IEnumerable<T>>> GetAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<ActionResult<T>> CreateAsync(T entity)
    {
        try
        {
            _unitOfWork.CreateTransaction();

            if (entity == null)
            {
                throw new ArgumentNullException("Entity");
            }

            if (_dbSet == null || _isDisposed)
            {
                _dbSet = _unitOfWork.Context.Set<T>();
            }

            _dbSet.Entry(entity).State = EntityState.Added;

            var result = await _dbSet.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Commit();

            return Ok(result.Entity);

        }
        catch (DbUpdateException dbu)
        {
            _unitOfWork.Rollback();

            _errorMessage = HandleUnitOfWorkException(dbu);
            throw new Exception(_errorMessage, dbu);
        }
    }

    public async Task<IActionResult> UpdateAsync(int id, T entity)
    {
        try
        {
            _unitOfWork.CreateTransaction();

            if (entity == null)
                throw new ArgumentNullException("Entity");

            if (_dbSet == null || _isDisposed)
                _dbSet = _unitOfWork.Context.Set<T>();

            var existingProduct = await _dbSet.FindAsync(id);
            if (existingProduct == null)
                return NotFound();

            _unitOfWork.Context.Entry(existingProduct).CurrentValues.SetValues(entity);

            await _unitOfWork.SaveChangesAsync();

            _unitOfWork.Commit();

            return Ok(existingProduct);
        }
        catch (DbUpdateException dbu)
        {
            _unitOfWork.Rollback();

            _errorMessage = HandleUnitOfWorkException(dbu);
            throw new Exception(_errorMessage, dbu);
        }
    }

    public async Task<IActionResult> DeleteAsync(int id)
    {
        try
        {
            if (id == null || id == 0)
                return NotFound();

            var data = await _dbSet.FindAsync(id);
            if (data == null)
                return NotFound();

            _dbSet.Remove(data);
            await _unitOfWork.SaveChangesAsync();
            return Ok(id);

        }
        catch (DbUpdateException dbu)
        {
            _errorMessage = HandleUnitOfWorkException(dbu);
            throw new Exception(_errorMessage, dbu);
        }
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
