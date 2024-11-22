using AutoMapper;
using MediatR;
using Product.Management.Application.Contracts.Persistence;
using Product.Management.Application.Exceptions;

namespace Product.Management.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public CreateProductCommandHandler(IMapper mapper, IProductsRepository productsRepository)
    {
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateProductCommandValidator(_productsRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid product data", validationResult);

        var productsObj = _mapper.Map<Domain.Products>(request);

        await _productsRepository.CreateAsync(productsObj);

        return productsObj.Id;
    }
}

