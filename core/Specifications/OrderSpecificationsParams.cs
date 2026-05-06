namespace core.Specifications;

public class OrderSpecificationParams : BaseSpecificationParams
{
    public string? StoreName { get; set; }
    public int? OrderNumber { get; set; }
    public DateOnly? Date { get; set; }
}
