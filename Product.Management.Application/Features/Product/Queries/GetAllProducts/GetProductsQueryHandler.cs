using AutoMapper;
using MediatR;
using Product.Management.Application.Contracts.Persistence;

namespace Product.Management.Application.Features.Product.Queries.GetAllProducts;
public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, List<ProductsDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public GetProductsQueryHandler(IMapper mapper,
        IProductsRepository productsRepository)
    {
        this._mapper = mapper;
        this._productsRepository = productsRepository;
    }

    public async Task<List<ProductsDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        // Query the database
        var lstProducts = await _productsRepository.GetAsync();

        // convert data objects to DTO objects
        var data = _mapper.Map<List<ProductsDto>>(lstProducts);

        // return list of DTO object
        return data;
    }
}
