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
        modelBuilder.Entity<IngredientSupplier>().HasKey(isu => new { isu.IngredientId, isu.SupplierId });

        modelBuilder.Entity<IngredientSupplier>()
            .HasOne(isu => isu.Ingredient)
            .WithMany(i => i.IngredientSuppliers)
            .HasForeignKey(isu => isu.IngredientId);

        modelBuilder.Entity<IngredientSupplier>()
            .HasOne(isu => isu.Supplier)
            .WithMany(s => s.IngredientSuppliers)
            .HasForeignKey(isu => isu.SupplierId);

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasOne(c => c.DeliveryAddress)
                  .WithMany()
                  .HasForeignKey("DeliveryAddressId")
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.BillingAddress)
                  .WithMany()
                  .HasForeignKey("BillingAddressId")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Product>().Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<IngredientSupplier>().Property(isu => isu.Price).HasColumnType("decimal(18,2)");
        modelBuilder.Entity<OrderItem>().Property(oi => oi.Price).HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Order>().Property(o => o.OrderNumber).ValueGeneratedOnAdd();

        modelBuilder.Entity<Order>().Property(o => o.OrderDate).HasConversion(
            dt => dt.ToUniversalTime(),
            dt => DateTime.SpecifyKind(dt, DateTimeKind.Utc)
        );

        base.OnModelCreating(modelBuilder);
    }
}
