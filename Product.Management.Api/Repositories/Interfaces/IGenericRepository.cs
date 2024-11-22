using Microsoft.AspNetCore.Mvc;
using Product.Management.Api.Models.Domain;

namespace Product.Management.Api.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ActionResult<IEnumerable<T>>> GetAsync();
        Task<ActionResult<T>> CreateAsync(T entity);
        Task<IActionResult> UpdateAsync(int id, T entity);
        Task<IActionResult> DeleteAsync(int id);

        Task<T> GetByIdAsync(int id);
    }
}