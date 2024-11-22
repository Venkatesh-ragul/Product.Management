using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Product.Management.Api.Contracts.Interfaces;
using FluentValidation;
using Product.Management.Api.Models.Domain;
using Product.Management.Api.UnitofWork;

namespace Product.Management.Api.Repositories.Implementation;

public class ProductsRepository : GenericRepository<Products>, IProductsRepository
{
    //private readonly ProductManagementContext _context;
    private readonly IMapper _mapper;
    private readonly IUnitOfwork _unitOfwork;
    protected DbSet<Products> dbSet;

    public ProductsRepository(IUnitOfwork unitOfwork) : base(unitOfwork)
    {
        _unitOfwork = unitOfwork;
        dbSet = _unitOfwork.Context.Set<Products>();
    }

    public async Task<IReadOnlyList<Products>> GetFilterProducts(string? query = null, string? sortBy = null, string? sortDirection = null,
        int? pageNumber = 1, int? pageSize = 10)
    {
        // Query
        var lstProducts = dbSet.AsQueryable();

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
        return await dbSet.AnyAsync(q => q.Name == name) == false;
    }
}
