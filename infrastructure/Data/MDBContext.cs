using core.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class MDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<IngredientSupplier> IngredientSuppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IngredientSupplier>().HasKey(p => new { p.IngredientId, p.SupplierId });
        base.OnModelCreating(modelBuilder);
    }
}
