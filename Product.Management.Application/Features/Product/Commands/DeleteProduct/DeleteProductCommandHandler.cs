using MediatR;
using Product.Management.Application.Contracts.Persistence;
using Product.Management.Application.Exceptions;

namespace Product.Management.Application.Features.Product.Commands.DeleteProduct;

public class DeleteProductCommandHandler(IProductsRepository productsRepository) : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IProductsRepository _productsRepository = productsRepository;

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // retrieve domain entity object
        var productToDelete = await _productsRepository.GetByIdAsync(request.Id);

        // verify that record exists
        if (productToDelete == null)
            throw new NotFoundException(nameof(Product), request.Id);

        // remove from database
        await _productsRepository.DeleteAsync(productToDelete);

        // retun record id
        return Unit.Value;
    }
}
