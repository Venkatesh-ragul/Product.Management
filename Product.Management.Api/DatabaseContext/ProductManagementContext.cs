using Microsoft.EntityFrameworkCore;
using Product.Management.Api.Models.Domain;

namespace Product.Management.Api.DatabaseContext
{
    public class ProductManagementContext(DbContextOptions<ProductManagementContext> options) : DbContext(options)
    {
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("ProductMgmtConnectionString");
        //}

        public DbSet<Products> ProductMgmt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductManagementContext).Assembly);

            modelBuilder.Entity<Products>()
                .Property(q => q.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Products>()
               .Property(q => q.Description)
               .IsRequired()
               .HasMaxLength(1000);

            modelBuilder.Entity<Products>()
               .Property(q => q.Category)
               .IsRequired()
               .HasMaxLength(100);

            modelBuilder.Entity<Products>()
               .Property(p => p.ProductPrice)
               .HasColumnType("decimal(18,4)");

            modelBuilder.Entity<Products>().HasData(
            new Products
            {
                Id = 1,
                Name = "Cheese",
                Description = "Amul Diced Cheese Blend Mozzeralla Cheddar",
                Category = "Dairy Products",
                ProductPrice = 116,
                ProductWeight = 200,
                Units = 20,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }
        );

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;

                if (entry.State == EntityState.Added)
                    entry.Entity.DateCreated = DateTime.Now;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
