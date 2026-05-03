using core.Entities;
using core.Entities.Orders;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class MDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<IngredientSupplier> IngredientSuppliers { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<IngredientSupplier>().HasKey(p => new { p.IngredientId, p.SupplierId });

        modelBuilder.Entity<Order>().Property(c => c.OrderDate).HasConversion(
            c => c.ToUniversalTime(),
            c => DateTime.SpecifyKind(c, DateTimeKind.Utc)
        );
        modelBuilder.Entity<Order>().Property(o => o.OrderNumber).ValueGeneratedOnAdd();

        base.OnModelCreating(modelBuilder);
    }
}
