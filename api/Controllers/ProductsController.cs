using infrastructure.Data;
using api.DTOs.Products;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController(MDBContext context) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var dto = await context.Products
            .Select(p => new GetAllProductsDto
            {
                Id = p.Id,
                ItemNumber = p.ItemNumber,
                ProductName = p.ProductName
            })
            .ToListAsync();

        return Resp(200, true, "Products retrieved", dto.Count, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindProduct(int id)
    {
        var product = await context.Products
            .Include(p => p.ProductSuppliers)
            .ThenInclude(ps => ps.Supplier)
            .SingleOrDefaultAsync(p => p.Id == id);

        if (product is null) return Resp(404, false, "Product not found");

        var dto = new GetProductDto
        {
            ItemNumber = product.ItemNumber,
            ProductName = product.ProductName,
            Suppliers = [.. product.ProductSuppliers.Select(ps => new GetProductSuppliersDto
            {
                SupplierName = ps.Supplier.SupplierName,
                Price = ps.Price
            })]
        };

        return Resp(200, true, "Product retrieved", dto.Suppliers.Count, dto);
    }

    [HttpGet("search/{itemNumber}")]
    public async Task<ActionResult> FindProduct(string itemNumber)
    {
        var product = await context.Products
            .Include(p => p.ProductSuppliers)
            .ThenInclude(ps => ps.Supplier)
            .SingleOrDefaultAsync(p => p.ItemNumber.ToLower().Trim() == itemNumber.ToLower().Trim());

        if (product is null) return Resp(404, false, "Product not found");

        var dto = new GetProductDto
        {
            ItemNumber = product.ItemNumber,
            ProductName = product.ProductName,
            Suppliers = [.. product.ProductSuppliers.Select(ps => new GetProductSuppliersDto
            {
                SupplierName = ps.Supplier.SupplierName,
                Price = ps.Price
            })]
        };

        return Resp(200, true, "Product retrieved", dto.Suppliers.Count, dto);
    }

    [HttpPost()]
    public async Task<ActionResult> AddProduct(PostProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (await context.Products.AnyAsync(p => p.ItemNumber.ToLower().Trim() == model.ItemNumber.ToLower().Trim()))
        {
            return Resp(409, false, "Item number in use");
        }

        var product = new Product
        {
            ItemNumber = model.ItemNumber,
            ProductName = model.ProductName
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        var dto = new GetProductDto
        {
            ItemNumber = product.ItemNumber,
            ProductName = product.ProductName,
            Suppliers = null
        };

        return CreatedAtResp(nameof(FindProduct), new { id = product.Id }, "Created", dto);
    }

    [HttpPost("suppliers/{supplierId}")]
    public async Task<ActionResult> AddProduct(int supplierId, PostProductSupplierDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.Price <= 0) return Resp(400, false, "price not in range");

        if (!await context.Suppliers.AnyAsync(s => s.Id == supplierId))
        {
            return Resp(404, false, "Supplier not found");
        }

        if (await context.Products.AnyAsync(p => p.ItemNumber.ToLower().Trim() == model.ItemNumber.ToLower().Trim()))
        {
            return Resp(409, false, "Item number in use");
        }

        var product = new Product
        {
            ItemNumber = model.ItemNumber,
            ProductName = model.ProductName
        };

        context.Products.Add(product);
        await context.SaveChangesAsync();

        var ps = new ProductSupplier
        {
            ProductId = product.Id,
            SupplierId = supplierId,
            Price = model.Price
        };

        context.ProductSuppliers.Add(ps);
        await context.SaveChangesAsync();

        var dto = new GetProductDto
        {
            ItemNumber = product.ItemNumber,
            ProductName = product.ProductName,
            Suppliers = null
        };

        return CreatedAtResp(nameof(FindProduct), new { id = product.Id }, $"Created for Supplier {supplierId}", dto);
    }
}
