using Microsoft.EntityFrameworkCore;
using Moq;
using Product.Management.Persistence.DatabaseContext;
using Shouldly;

namespace Product.Management.Persistence.IntegrationTests;

public class ProductManagementContextTests
{
    private ProductManagementContext _productManagementContext;

    public ProductManagementContextTests()
    {
        //var dbOptions = new DbContextOptionsBuilder<ProductManagementContext>()
        //    .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        //_productManagementContext = new ProductManagementContext(dbOptions);
    }

    [Fact]
    public void Test1()
    {

    }
}