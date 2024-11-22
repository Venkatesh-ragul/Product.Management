using FluentValidation;
using Product.Management.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Application.Features.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductsRepository _productsRepository;

    public CreateProductCommandValidator(IProductsRepository productsRepository)
    {
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

        RuleFor(q => q)
            .MustAsync(ProductNameUnique)
            .WithMessage("Product name already exists");

        this._productsRepository = productsRepository;
    }

    private Task<bool> ProductNameUnique(CreateProductCommand command, CancellationToken token)
    {
        return _productsRepository.IsProductNameUnique(command.Name);
    }
}
