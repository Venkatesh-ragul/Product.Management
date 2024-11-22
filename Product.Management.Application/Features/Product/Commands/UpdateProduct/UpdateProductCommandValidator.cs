using FluentValidation;
using Product.Management.Application.Contracts.Persistence;

namespace Product.Management.Application.Features.Product.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductsRepository _productsRepository;

    public UpdateProductCommandValidator(IProductsRepository productsRepository)
    {
        RuleFor(q => q.Id)
            .NotNull()
            .MustAsync(ProductMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

        RuleFor(p => p.Description)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(1000).WithMessage("{PropertyName} must be fewer than 1000 characters");

        RuleFor(p => p.Category)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(100).WithMessage("{PropertyName} must be fewer than 100 characters");

        RuleFor(p => p.ProductPrice)
            .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(p => p.ProductWeight)
        .GreaterThanOrEqualTo(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(p => p.Units)
            .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} cannot be less than 0");

        this._productsRepository = productsRepository;
    }

    private async Task<bool> ProductMustExist(int id, CancellationToken arg2)
    {
        var productMust = await _productsRepository.GetByIdAsync(id);
        return productMust != null;
    }
}
