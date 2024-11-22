using Microsoft.EntityFrameworkCore;
using Product.Management.Application.Contracts.Persistence;
using Product.Management.Domain;
using Product.Management.Persistence.DatabaseContext;

namespace Product.Management.Persistence.Repositories;

public class ProductsRepository(ProductManagementContext context) : GenericRepository<Products>(context), IProductsRepository
{
    public async Task<IEnumerable<Products>> GetFilterProducts(string? query = null, string? sortBy = null, string? sortDirection = null,
        int? pageNumber = 1, int? pageSize = 10)
    {
        // Query
        var lstProducts = _context.ProductMgmt.AsQueryable();

        // Filtering
        if (string.IsNullOrWhiteSpace(query) == false)
            lstProducts = lstProducts.Where(o => o.Name.Contains(query) || o.Category.Contains(query));

        // Sorting
        if (string.IsNullOrWhiteSpace(sortBy) == false)
        {
            if (string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
            {
                var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);

                lstProducts = isAsc ? lstProducts.OrderBy(x => x.Name) : lstProducts.OrderByDescending(x => x.Name);
            }

            if (string.Equals(sortBy, "Category", StringComparison.OrdinalIgnoreCase))
            {
                var isAsc = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);

                lstProducts = isAsc ? lstProducts.OrderBy(x => x.Category) : lstProducts.OrderByDescending(x => x.Category);
            }
        }

        // Pagination
        var skipResults = (pageNumber - 1) * pageSize;
        lstProducts = lstProducts.Skip(skipResults ?? 0).Take(pageSize ?? 100);

        return await lstProducts.ToListAsync();
    }

    public async Task<bool> IsProductNameUnique(string name)
    {
        return await _context.ProductMgmt.AnyAsync(q => q.Name == name) == false;
    }
}
