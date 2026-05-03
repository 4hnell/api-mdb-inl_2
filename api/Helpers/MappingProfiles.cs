using api.DTOs.Customers;
using api.DTOs.Ingredients;
using api.DTOs.Orders;
using api.DTOs.Products;
using api.DTOs.Suppliers;
using AutoMapper;
using core.Entities;
using core.Entities.Orders;

namespace api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Ingredient, GetAllIngredientsDto>();
        CreateMap<PostIngredientDto, Ingredient>();
        CreateMap<PostIngredientSupplierDto, Ingredient>();
        CreateMap<PostIngredientSupplierDto, IngredientSupplier>();
        CreateMap<Ingredient, GetIngredientDto>()
            .ForMember(d => d.Suppliers, o => o.MapFrom(s => s.IngredientSuppliers));
        CreateMap<IngredientSupplier, GetIngredientSuppliersDto>()
            .ForMember(d => d.SupplierName, o => o.MapFrom(s => s.Supplier.SupplierName))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));

        CreateMap<Supplier, GetAllSuppliersDto>();
        CreateMap<Supplier, GetSupplierDto>();
        CreateMap<PostSupplierIngredientDto, IngredientSupplier>();
        CreateMap<Supplier, GetSupplierSellingDto>()
            .ForMember(d => d.Ingredients, o => o.MapFrom(s => s.IngredientSuppliers));
        CreateMap<IngredientSupplier, GetIngredientsSupplierDto>()
            .ForMember(d => d.ItemNumber, o => o.MapFrom(s => s.Ingredient.ItemNumber))
            .ForMember(d => d.IngredientName, o => o.MapFrom(s => s.Ingredient.IngredientName))
            .ForMember(d => d.Price, o => o.MapFrom(s => s.Price));

        CreateMap<Customer, GetAllCustomersDto>();
        CreateMap<Customer, GetCustomerDto>();
        CreateMap<Address, GetCustomerAddressDto>().ReverseMap();
        CreateMap<PostCustomerDto, Customer>();

        CreateMap<Product, GetAllProductsDto>();
        CreateMap<Product, GetProductDto>();
        CreateMap<PostProductDto, Product>();

        CreateMap<Order, GetAllOrdersDto>();
        CreateMap<Order, GetOrderDto>();
        CreateMap<OrderItem, GetOrderItemDto>();
        CreateMap<PostOrderDto, Order>();
        CreateMap<PostOrderItemDto, OrderItem>();
    }
}
