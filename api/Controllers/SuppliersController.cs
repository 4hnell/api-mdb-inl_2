using infrastructure.Data;
using api.DTOs.Suppliers;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[Route("api/suppliers")]
[ApiController]
public class SuppliersController(MDBContext context) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers()
    {
        var dto = await context.Suppliers
            .Select(s => new GetAllSuppliersDto
            {
                Id = s.Id,
                SupplierName = s.SupplierName,
                Phone = s.Phone,
                Email = s.Email,

            })
            .ToListAsync();

        return Resp(200, true, "Suppliers retrieved", dto.Count, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {
        var supplier = await context.Suppliers.FindAsync(id);

        if (supplier is null) return Resp(404, false, "Supplier not found");

        var dto = new GetSupplierDto
        {
            SupplierName = supplier.SupplierName,
            Phone = supplier.Phone,
            Email = supplier.Email,
            AddressLine = supplier.AddressLine,
            PostalCode = supplier.PostalCode,
            City = supplier.City,
            Contact = supplier.Contact
        };

        return Resp(200, true, "Supplier retrieved", data: dto);
    }

    [HttpGet("search/{supplierName}")]
    public async Task<ActionResult> FindSupplier(string supplierName)
    {
        var supplier = await context.Suppliers
            .Include(s => s.ProductSuppliers)
            .ThenInclude(ps => ps.Product)
            .SingleOrDefaultAsync(s => s.SupplierName.ToLower().Trim() == supplierName.ToLower().Trim());

        if (supplier is null) return Resp(404, false, "Supplier not found");

        var dto = new GetSupplierSearchDto
        {
            SupplierName = supplier.SupplierName,
            Phone = supplier.Phone,
            Email = supplier.Email,
            Products = [.. supplier.ProductSuppliers.Select(ps => new GetProductsSupplierDto
            {
                ItemNumber = ps.Product.ItemNumber,
                ProductName = ps.Product.ProductName,
                Price = ps.Price
            })]
        };

        return Resp(200, true, "Supplier retrieved", dto.Products.Count, dto);
    }

    [HttpPost("{supplierId}/products/{productId}")]
    public async Task<ActionResult> PostSupplierProduct(int supplierId, int productId, PostSupplierProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.Price <= 0) return Resp(400, false, "price not in range");

        if (!await context.Suppliers.AnyAsync(s => s.Id == supplierId))
        {
            return Resp(404, false, "Supplier not found");
        }

        if (!await context.Products.AnyAsync(p => p.Id == productId))
        {
            return Resp(404, false, "Product not found");
        }

        if (await context.ProductSuppliers.AnyAsync(ps => ps.ProductId == productId && ps.SupplierId == supplierId))
        {
            return Resp(409, false, "Product and Supplier already linked");
        }

        var ps = new ProductSupplier
        {
            ProductId = productId,
            SupplierId = supplierId,
            Price = model.Price
        };

        context.ProductSuppliers.Add(ps);
        await context.SaveChangesAsync();

        return Resp(201, true, "Created", data: new
        {
            Details = $"Product {productId} linked with Supplier {supplierId}",
            model.Price
        });
    }

    [HttpPatch("{supplierId}/products/{productId}")]
    public async Task<ActionResult> PatchSupplierProduct(int supplierId, int productId, PatchSupplierProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.Price <= 0) return Resp(400, false, "price not in range");

        if (!await context.Suppliers.AnyAsync(s => s.Id == supplierId))
        {
            return Resp(404, false, "Supplier not found");
        }

        if (!await context.Products.AnyAsync(p => p.Id == productId))
        {
            return Resp(404, false, "Product not found");
        }

        var ps = await context.ProductSuppliers.FindAsync(productId, supplierId);

        if (ps is null) return Resp(404, false, "Product and Supplier not linked");

        ps.Price = model.Price;

        await context.SaveChangesAsync();

        return NoContent();
    }
}
