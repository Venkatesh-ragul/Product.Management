using Product.Management.Domain.Common;

namespace Product.Management.Domain;

public class Products : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal ProductPrice { get; set; }
    public double ProductWeight { get; set; }
    public double Units { get; set; }
}
