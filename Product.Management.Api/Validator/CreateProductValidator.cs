﻿using Product.Management.Api.Contracts.Interfaces;
using FluentValidation;
using Product.Management.Api.Models;
using Product.Management.Api.Models.Domain;

namespace Product.Management.Api.Validator;

internal class CreateProductValidator : AbstractValidator<CreateProductRequest>
{
    private IProductsRepository _productsRepository;

    public CreateProductValidator(IProductsRepository productsRepository)
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

    private Task<bool> ProductNameUnique(CreateProductRequest objProducts, CancellationToken token)
    {
        return _productsRepository.IsProductNameUnique(objProducts.Name);
    }
}