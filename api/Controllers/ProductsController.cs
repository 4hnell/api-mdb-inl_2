using api.DTOs.Products;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using core.Specifications;
using core.Interfaces;
using api.Helpers;
using AutoMapper;
using core.Entities.Orders;
using api.DTOs.Customers;

namespace api.Controllers;

public class ProductsController(IUnitOfWork uow, IMapper mapper) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts([FromQuery] ProductSpecificationParams args)
    {
        var products = await CreateResult(uow.Repository<Product>(), new ProductSpecification(args));
        var mappedProducts = mapper.Map<IReadOnlyList<GetAllProductsDto>>(products.Result);

        return Resp(200, true, "Products retrieved", products, mappedProducts);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProductById(string id)
    {
        var product = await uow.Repository<Product>().FindAsync(new ProductSpecification(productId: id));

        if (product is null) return Resp(404, false, "Product not found");

        var mappedProduct = mapper.Map<GetProductDto>(product);

        return Resp(200, true, "Product found", new DataResult<Product>(1, [product]), [mappedProduct]);
    }

    [HttpGet("{productName}/bought-by")]
    public async Task<ActionResult> FindProductAndBuyers(string productName)
    {
        var product = await uow.Repository<Product>().FindAsync(new ProductSpecification(productName: productName));

        if (product is null) return Resp(404, false, "Product not found");

        var buyers = await uow.Repository<Customer>().ListAsync(new CustomerSpecification(productName: productName));
        var mappedBuyers = mapper.Map<List<GetAllCustomersDto>>(buyers);
        var mappedProduct = mapper.Map<GetProductBuyersDto>(product);
        mappedProduct.Buyers = mappedBuyers;

        return Resp(200, true, "Product found, also listing buyers", new DataResult<Product>(1, [product]), [mappedProduct]);
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (await uow.Repository<Product>().AnyAsync(new ProductSpecification(productName: model.ProductName)))
        {
            return Resp(409, false, "Product already exists");
        }

        var product = mapper.Map<Product>(model);

        uow.Repository<Product>().Add(product);
        await uow.Complete();

        var mappedProduct = mapper.Map<GetProductDto>(product);

        return CreatedAtResp(
            nameof(FindProductById),
            new { id = product.Id },
            "Created",
            new DataResult<Product>(1, [product]),
            [mappedProduct]
        );
    }

    [HttpPatch("update-price")]
    public async Task<ActionResult> PatchProductPrice(PatchProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.UnitPrice <= 0) return Resp(400, false, "Price not in range");

        var product = await uow.Repository<Product>().FindAsync(new ProductSpecification(productName: model.ProductName));

        if (product is null) return Resp(404, false, "Product not found");

        product.UnitPrice = model.UnitPrice;

        await uow.Complete();

        return NoContent();
    }
}
