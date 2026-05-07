using System.Text.Json;
using core.Entities;
using core.Entities.Orders;
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

    public static async Task SeedIngredients(MDBContext context)
    {
        if (context.Ingredients.Any()) return;

        var ingredientsFromJson = File.ReadAllText("../infrastructure/Data/Json/ingredients.json");
        var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(ingredientsFromJson, options);

        if (ingredients is not null && ingredients.Count > 0)
        {
            await context.Ingredients.AddRangeAsync(ingredients);
            await context.SaveChangesAsync();
        }
    }

    public static async Task SeedIngredientSuppliers(MDBContext context)
    {
        if (context.IngredientSuppliers.Any()) return;

        var ingredientSuppliersFromJson = File.ReadAllText("../infrastructure/Data/Json/ingredientsuppliers.json");
        var psDtos = JsonSerializer.Deserialize<List<IngredientSupplierSeedDto>>(ingredientSuppliersFromJson, options);

        if (psDtos is not null)
        {
            var ingredients = await context.Ingredients.ToListAsync();
            var suppliers = await context.Suppliers.ToListAsync();
            var ingredientSuppliers = new List<IngredientSupplier>();

            foreach (var psDto in psDtos)
            {
                var ingredient = ingredients.FirstOrDefault(i => i.ItemNumber == psDto.ItemNumber);
                var supplier = suppliers.FirstOrDefault(s => s.SupplierName == psDto.SupplierName);

                if (ingredient is not null && supplier is not null)
                {
                    ingredientSuppliers.Add(new IngredientSupplier
                    {
                        IngredientId = ingredient.Id,
                        SupplierId = supplier.Id,
                        Price = psDto.Price
                    });
                }
            }

            if (ingredientSuppliers is not null && ingredientSuppliers.Count > 0)
            {
                await context.IngredientSuppliers.AddRangeAsync(ingredientSuppliers);
                await context.SaveChangesAsync();
            }
        }
    }

    public static async Task SeedCustomers(MDBContext context)
    {
        if (context.Customers.Any()) return;

        var customersFromJson = File.ReadAllText("../infrastructure/Data/Json/customers.json");
        var customers = JsonSerializer.Deserialize<List<Customer>>(customersFromJson, options);

        if (customers is not null && customers.Count > 0)
        {
            await context.Customers.AddRangeAsync(customers);
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

    public static async Task SeedOrders(MDBContext context)
    {
        if (context.Orders.Any()) return;

        var ordersFromJson = File.ReadAllText("../infrastructure/Data/Json/orders.json");
        var orderDtos = JsonSerializer.Deserialize<List<OrderSeedDto>>(ordersFromJson, options);

        if (orderDtos is not null)
        {
            var customers = await context.Customers.ToListAsync();
            var orders = new List<Order>();

            foreach (var orderDto in orderDtos)
            {
                var customer = customers.FirstOrDefault(c => c.StoreName == orderDto.StoreName);

                if (customer is not null)
                {
                    orders.Add(new Order
                    {
                        CustomerId = customer.Id,
                        StoreName = customer.StoreName,
                        OrderNumber = orderDto.OrderNumber,
                        OrderDate = orderDto.OrderDate,
                        OrderItems = [.. orderDto.OrderItems.Select(oi => new OrderItem
                        {
                            ProductName = oi.ProductName,
                            Quantity = oi.Quantity,
                            Price = oi.Price
                        })]
                    });
                }
            }

            if (orders is not null && orders.Count > 0)
            {
                await context.Orders.AddRangeAsync(orders);
                await context.SaveChangesAsync();
            }
        }
    }
}
