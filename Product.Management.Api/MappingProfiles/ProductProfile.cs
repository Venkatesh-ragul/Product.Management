using AutoMapper;
using Product.Management.Api.Models.Domain;
using Product.Management.Api.Models.DTO;

namespace Product.Management.Application.MappingProfiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductsDto, Products>().ReverseMap();
        CreateMap<ProductsDetailsDto, Products>().ReverseMap();

        CreateMap<CreateProductRequest, Products>();
        CreateMap<UpdateProductRequest, Products>().ReverseMap();
    }
}
