using Product.Management.Domain;
using Product.Management.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Application.Contracts.Persistence;

public interface IProductsRepository : IGenericRepository<Products>
{
    Task<IEnumerable<Products>> GetFilterProducts(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 100);

    Task<bool> IsProductNameUnique(string name);
}
