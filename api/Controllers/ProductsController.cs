using api.DTOs.Products;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using core.Specifications;
using core.Interfaces;
using api.Helpers;
using AutoMapper;

namespace api.Controllers;

public class ProductsController(IUnitOfWork uow, IMapper mapper) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts([FromQuery] ProductSpecificationParams args)
    {
        var data = await CreateResult(uow.Repository<Product>(), new ProductSpecification(args));
        var mappedData = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<GetAllProductsDto>>(data.Result);

        return Resp(200, true, "Products retrieved", data, mappedData);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProductById(string id)
    {
        var product = await uow.Repository<Product>().FindByIdAsync(id);

        if (product is null) return Resp(404, false, "Product not found");

        var mappedProduct = mapper.Map<Product, GetProductDto>(product);

        return Resp(200, true, "Product found", new DataResult<Product>(1, [product]), [mappedProduct]);
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (await uow.Repository<Product>().AnyAsync(p => p.ItemNumber.ToLower().Trim() == model.ItemNumber.ToLower().Trim()))
        {
            return Resp(409, false, "Item number in use");
        }

        var product = mapper.Map<PostProductDto, Product>(model);

        uow.Repository<Product>().Add(product);
        await uow.Complete();

        var mappedProduct = mapper.Map<Product, GetProductDto>(product);

        return CreatedAtResp(
            nameof(FindProductById),
            new { id = product.Id },
            "Created",
            new DataResult<Product>(1, [product]),
            [mappedProduct]
        );
    }

    [HttpPost("with-supplier")]
    public async Task<ActionResult> AddProduct(PostProductSupplierDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.Price <= 0) return Resp(400, false, "Price not in range");

        if (!await uow.Repository<Supplier>().AnyAsync(s => s.Id == model.SupplierId))
        {
            return Resp(404, false, "Supplier not found");
        }

        if (await uow.Repository<Product>().AnyAsync(p => p.ItemNumber.ToLower().Trim() == model.ItemNumber.ToLower().Trim()))
        {
            return Resp(409, false, "Item number in use");
        }

        var product = mapper.Map<PostProductSupplierDto, Product>(model);

        uow.Repository<Product>().Add(product);
        await uow.Complete();

        var ps = mapper.Map<PostProductSupplierDto, ProductSupplier>(model);

        uow.Repository<ProductSupplier>().Add(ps);
        await uow.Complete();

        var mappedProduct = mapper.Map<Product, GetProductDto>(product);

        return CreatedAtResp(
            nameof(FindProductById),
            new { id = product.Id },
            $"Created for Supplier {model.SupplierId}",
            new DataResult<Product>(1, [product]),
            [mappedProduct]
        );
    }
}
