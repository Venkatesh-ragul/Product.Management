using MediatR;
using Product.Management.Application.Features.Product.Queries.GetAllProducts;

namespace Product.Management.Application.Features.Product.Queries.GetFilterProducts;

public record GetFilterProductsQuery(string? Query, string? SortBy, string? SortDirection, int? PageNumber, int? PageSize) : IRequest<List<ProductsDto>>;
