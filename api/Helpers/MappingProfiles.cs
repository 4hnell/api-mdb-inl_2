using api.DTOs.Products;
using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;

namespace api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, GetAllProductsDto>();
        CreateMap<Product, GetProductDto>();
        CreateMap<PostProductDto, Product>();
        CreateMap<PostProductSupplierDto, Product>();
        CreateMap<PostProductSupplierDto, ProductSupplier>();
        CreateMap<Supplier, GetAllSuppliersDto>();
        CreateMap<Supplier, GetSupplierDto>();
        CreateMap<PostSupplierProductDto, ProductSupplier>();
        CreateMap<Product, GetProductDto>()
            .ForMember(d => d.Suppliers, o => o.MapFrom(s => s.ProductSuppliers));
        CreateMap<ProductSupplier, GetProductSuppliersDto>()
            .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.SupplierName))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));
        CreateMap<Supplier, GetSupplierSearchDto>()
            .ForMember(d => d.Products, o => o.MapFrom(s => s.ProductSuppliers));
        CreateMap<ProductSupplier, GetProductsSupplierDto>()
            .ForMember(d => d.ItemNumber, o => o.MapFrom(s => s.Product.ItemNumber))
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));
    }
}
