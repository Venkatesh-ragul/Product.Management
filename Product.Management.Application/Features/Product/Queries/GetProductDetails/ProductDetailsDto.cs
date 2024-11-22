using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Application.Features.Product.Queries.GetAllProducts;

public class ProductDetailsDto
{
	public int Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string Category { get; set; } = string.Empty;
	public decimal ProductPrice { get; set; }
	public double ProductWeight { get; set; }
	public double Units { get; set; }
	public DateTime? DateCreated { get; set; }
	public DateTime? DateModified { get; set; }
}
