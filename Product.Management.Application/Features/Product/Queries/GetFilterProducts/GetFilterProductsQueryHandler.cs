using AutoMapper;
using MediatR;
using Product.Management.Application.Contracts.Persistence;
using Product.Management.Application.Exceptions;
using Product.Management.Application.Features.Product.Queries.GetAllProducts;
using Product.Management.Application.Features.Product.Queries.GetProductDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Application.Features.Product.Queries.GetFilterProducts;

public class GetFilterProductsQueryHandler : IRequestHandler<GetFilterProductsQuery, List<ProductsDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public GetFilterProductsQueryHandler(IMapper mapper, IProductsRepository productsRepository)
    {
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<List<ProductsDto>> Handle(GetFilterProductsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var lstProducts = await _productsRepository.GetFilterProducts(request.Query, request.SortBy, request.SortDirection, request.PageNumber, request.PageSize);

        // verify that record exists
        if (lstProducts == null)
            throw new NotFoundException(nameof(lstProducts), request);

        // convert data object to DTO object
        var data = _mapper.Map<List<ProductsDto>>(lstProducts);

        // return DTO object
        return data;
    }
}
