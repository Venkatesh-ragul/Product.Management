using Product.Management.Api.Models.Domain;
using Product.Management.Api.Repositories.Interfaces;

namespace Product.Management.Api.Contracts.Interfaces;

public interface IProductsRepository : IGenericRepository<Products>
{
    Task<IReadOnlyList<Products>> GetFilterProducts(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100);

    Task<bool> IsProductNameUnique(string name);
}
