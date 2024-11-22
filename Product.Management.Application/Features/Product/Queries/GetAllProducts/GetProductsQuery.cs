using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Application.Features.Product.Queries.GetAllProducts;

public record GetProductsQuery : IRequest<List<ProductsDto>>;
