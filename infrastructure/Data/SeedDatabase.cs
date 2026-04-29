using System.Text.Json;
using core.Entities;

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

        var suppliersFromJson = File.ReadAllText("Data/Json/suppliers.json");
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

        var productsFromJson = File.ReadAllText("Data/Json/products.json");
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

        var productSuppliersFromJson = File.ReadAllText("Data/Json/productsuppliers.json");
        var productSuppliers = JsonSerializer.Deserialize<List<ProductSupplier>>(productSuppliersFromJson, options);

        if (productSuppliers is not null && productSuppliers.Count > 0)
        {
            await context.ProductSuppliers.AddRangeAsync(productSuppliers);
            await context.SaveChangesAsync();
        }
    }
}
