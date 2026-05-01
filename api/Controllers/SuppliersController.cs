using api.DTOs.Suppliers;
using core.Entities;
using Microsoft.AspNetCore.Mvc;
using core.Specifications;
using core.Interfaces;
using api.Helpers;
using AutoMapper;

namespace api.Controllers;

public class SuppliersController(IUnitOfWork uow, IMapper mapper) : MDBBaseController
{
    [HttpGet()]
    public async Task<ActionResult> ListAllSuppliers([FromQuery] SupplierSpecificationParams args)
    {
        var data = await CreateResult(uow.Repository<Supplier>(), new SupplierSpecification(args));
        var mappedData = mapper.Map<IReadOnlyList<GetAllSuppliersDto>>(data.Result);

        return Resp(200, true, "Suppliers retrieved", data, mappedData);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplierById(string id)
    {
        var supplier = await uow.Repository<Supplier>().FindByIdAsync(id);

        if (supplier is null) return Resp(404, false, "Supplier not found");

        var mappedSupplier = mapper.Map<GetSupplierDto>(supplier);

        return Resp(200, true, "Supplier found", new DataResult<Supplier>(1, [supplier]), [mappedSupplier]);
    }

    [HttpGet("{supplierName}/product-list")]
    public async Task<ActionResult> FindSupplierWithProducts(string supplierName)
    {
        var supplier = await uow.Repository<Supplier>().FindAsync(new SupplierSpecification(supplierName: supplierName));

        if (supplier is null) return Resp(404, false, "Supplier not found");

        var mappedData = mapper.Map<GetSupplierSearchDto>(supplier);

        return Resp(200, true, "Supplier and products found", new DataResult<Supplier>(1, [supplier]), [mappedData]);
    }

    [HttpPost("assign-product")]
    public async Task<ActionResult> PostSupplierProduct(PostSupplierProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.Price <= 0) return Resp(400, false, "Price not in range");

        if (!await uow.Repository<Supplier>().AnyAsync(new SupplierSpecification(supplierId: model.SupplierId)))
        {
            return Resp(404, false, "Supplier not found");
        }

        if (!await uow.Repository<Product>().AnyAsync(new ProductSpecification(productId: model.ProductId)))
        {
            return Resp(404, false, "Product not found");
        }

        if (await uow.Repository<ProductSupplier>().AnyAsync(new ProductSupplierSpecification(model.ProductId, model.SupplierId)))
        {
            return Resp(409, false, "Product and supplier already linked");
        }

        var ps = mapper.Map<ProductSupplier>(model);

        uow.Repository<ProductSupplier>().Add(ps);
        await uow.Complete();

        return Resp(201, true, "Product added to supplier");
    }

    [HttpPatch("update-price")]
    public async Task<ActionResult> PatchSupplierProduct(PatchSupplierProductDto model)
    {
        if (model is null) return Resp(400, false, "Invalid input");

        if (model.Price <= 0) return Resp(400, false, "Price not in range");

        if (!await uow.Repository<Supplier>().AnyAsync(new SupplierSpecification(supplierId: model.SupplierId)))
        {
            return Resp(404, false, "Supplier not found");
        }

        if (!await uow.Repository<Product>().AnyAsync(new ProductSpecification(productId: model.ProductId)))
        {
            return Resp(404, false, "Product not found");
        }

        var ps = await uow.Repository<ProductSupplier>().FindAsync(new ProductSupplierSpecification(model.ProductId, model.SupplierId));

        if (ps is null) return Resp(404, false, "Product not sold by supplier");

        ps.Price = model.Price;

        await uow.Complete();

        return NoContent();
    }
}
