using MarketPlace.Infrastructure.Data.Models;
using MarketPlace.Infrastructure.Data.SeedDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MarketPlace.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ProductBuyer>()
                .HasKey(pb => new { pb.BuyerId, pb.ProductId });

            builder.Entity<ProductBuyer>()
                 .HasOne(pb => pb.Buyer)
                 .WithMany()
                 .HasForeignKey(pb => pb.BuyerId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductBuyer>()
                .HasOne(pb => pb.Product)
                .WithMany()
                .HasForeignKey(pb => pb.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
 
            base.OnModelCreating(builder);
        }
        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ShipingAddress> ShipingAddresses { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<ProductBuyer> ProductBuyers { get; set; } = null!;

        public DbSet<ProductRating> ProductRatings { get; set; } = null!;
    }
}
