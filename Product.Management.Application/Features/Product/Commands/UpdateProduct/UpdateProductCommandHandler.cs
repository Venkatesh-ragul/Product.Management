using AutoMapper;
using MediatR;
using Product.Management.Application.Contracts.Persistence;
using Product.Management.Application.Exceptions;

namespace Product.Management.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly IProductsRepository _productsRepository;

    public UpdateProductCommandHandler(IMapper mapper, IProductsRepository productsRepository)
    {
        _mapper = mapper;
        _productsRepository = productsRepository;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator(_productsRepository);
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult.Errors.Any())
            throw new BadRequestException("Invalid product details.", validationResult);

        var existProduct = await _productsRepository.GetByIdAsync(request.Id);
        if (existProduct is null)
            throw new NotFoundException(nameof(Product), request.Id);

        _mapper.Map(request, existProduct);

        await _productsRepository.UpdateAsync(existProduct);

        return Unit.Value;
    }
}
