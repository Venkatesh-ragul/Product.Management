using AutoMapper;
using MediatR;
using Product.Management.Application.Contracts.Persistence;
using Product.Management.Application.Exceptions;
using Product.Management.Application.Features.Product.Queries.GetAllProducts;
 
namespace Product.Management.Application.Features.Product.Queries.GetProductDetails;

public class GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, ProductDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public GetProductDetailsQueryHandler(IMapper mapper, IProductsRepository productsRepository)
    {
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<ProductDetailsDto> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var productDetails = await _productsRepository.GetByIdAsync(request.Id);

        // verify that record exists
        if (productDetails == null)
            throw new NotFoundException(nameof(productDetails), request.Id);

        // convert data object to DTO object
        var data = _mapper.Map<ProductDetailsDto>(productDetails);

        // return DTO object
        return data;
    }
}
