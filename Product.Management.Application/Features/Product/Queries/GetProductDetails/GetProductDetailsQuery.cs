using MediatR;
using Product.Management.Application.Features.Product.Queries.GetAllProducts;

namespace Product.Management.Application.Features.Product.Queries.GetProductDetails;

public record GetProductDetailsQuery(int Id) : IRequest<ProductDetailsDto>;
