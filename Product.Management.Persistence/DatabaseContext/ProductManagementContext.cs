using Microsoft.EntityFrameworkCore;
using Product.Management.Domain;
using Product.Management.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Management.Persistence.DatabaseContext
{
    public class ProductManagementContext(DbContextOptions<ProductManagementContext> options) : DbContext(options)
    {
        public DbSet<Products> ProductMgmt { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductManagementContext).Assembly);

            modelBuilder.Entity<Products>()
                .Property(p => p.ProductPrice)
                .HasColumnType("decimal(18,4)");

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
