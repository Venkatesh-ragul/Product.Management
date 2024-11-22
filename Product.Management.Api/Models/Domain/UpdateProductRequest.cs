namespace Product.Management.Api.Models.Domain
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public decimal ProductPrice { get; set; }
        public double ProductWeight { get; set; }
        public double Units { get; set; }
    }
}
