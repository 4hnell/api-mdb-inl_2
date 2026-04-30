using core.Entities;

namespace core.Specifications;

public class ProductSupplierSpecification(string supplierId, string productId) : BaseSpecification<ProductSupplier>(c =>
        c.ProductId == productId && c.SupplierId == supplierId)
{ }
