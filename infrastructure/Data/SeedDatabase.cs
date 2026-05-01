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
                var ingredient = ingredients.FirstOrDefault(p => p.ItemNumber == psDto.ItemNumber);
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
}
