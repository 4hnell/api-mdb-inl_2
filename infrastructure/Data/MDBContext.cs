using core.Entities;
using Microsoft.EntityFrameworkCore;

namespace infrastructure.Data;

public class MDBContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductSupplier>().HasKey(p => new { p.ProductId, p.SupplierId });
        base.OnModelCreating(modelBuilder);
    }
}
