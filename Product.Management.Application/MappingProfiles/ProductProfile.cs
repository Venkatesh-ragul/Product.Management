using AutoMapper;
using Product.Management.Application.Features.Product.Commands.CreateProduct;
using Product.Management.Application.Features.Product.Commands.UpdateProduct;
using Product.Management.Application.Features.Product.Queries.GetAllProducts;
using Product.Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductsDto, Products>().ReverseMap();
        CreateMap<Products, ProductDetailsDto>();

        CreateMap<CreateProductCommand, Products>();
        CreateMap<UpdateProductCommand, Products>();
    }
}
