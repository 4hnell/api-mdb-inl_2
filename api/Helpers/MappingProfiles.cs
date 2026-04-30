using api.DTOs.Products;
using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;

namespace api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<IReadOnlyList<Product>, IReadOnlyList<GetAllProductsDto>>();
        CreateMap<Product, GetProductDto>();
        CreateMap<PostProductDto, Product>();
        CreateMap<PostProductSupplierDto, Product>();
        CreateMap<PostProductSupplierDto, ProductSupplier>();
        CreateMap<IReadOnlyList<Supplier>, IReadOnlyList<GetAllSuppliersDto>>();
        CreateMap<Supplier, GetSupplierDto>();
        CreateMap<PostSupplierProductDto, ProductSupplier>();
    }
}
