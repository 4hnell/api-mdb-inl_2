using System.Text.Json;
using core.Entities;
using infrastructure.Data.SeedDTOs;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class SeedDatabase()
{
    private static readonly JsonSerializerOptions options = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public static async Task SeedSuppliers(MDBContext context)
    {
        if (context.Suppliers.Any()) return;

        var suppliersFromJson = File.ReadAllText("../Infrastructure/Data/Json/suppliers.json");
        var suppliers = JsonSerializer.Deserialize<List<Supplier>>(suppliersFromJson, options);

        if (suppliers is not null && suppliers.Count > 0)
        {
            await context.Suppliers.AddRangeAsync(suppliers);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedProducts(MDBContext context)
    {
        if (context.Products.Any()) return;

        var productsFromJson = File.ReadAllText("../infrastructure/Data/Json/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(productsFromJson, options);

        if (products is not null && products.Count > 0)
        {
            await context.Products.AddRangeAsync(products);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedProductSuppliers(MDBContext context)
    {
        if (context.ProductSuppliers.Any()) return;

        var productSuppliersFromJson = File.ReadAllText("../infrastructure/Data/Json/productsuppliers.json");
        var psDtos = JsonSerializer.Deserialize<List<ProductSupplierSeedDto>>(productSuppliersFromJson, options);

        if (psDtos is not null)
        {
            var products = await context.Products.ToListAsync();
            var suppliers = await context.Suppliers.ToListAsync();

            var productSuppliers = new List<ProductSupplier>();

            foreach (var psDto in psDtos)
            {
                var product = products.FirstOrDefault(p => p.ItemNumber == psDto.ItemNumber);
                var supplier = suppliers.FirstOrDefault(s => s.SupplierName == psDto.SupplierName);

                if (product is not null && supplier is not null)
                {
                    productSuppliers.Add(new ProductSupplier
                    {
                        ProductId = product.Id,
                        SupplierId = supplier.Id,
                        Price = psDto.Price
                    });
                }
            }

            if (productSuppliers is not null && productSuppliers.Count > 0)
            {
                await context.ProductSuppliers.AddRangeAsync(productSuppliers);
                await context.SaveChangesAsync();
            }
        }
    }
}
