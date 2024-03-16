using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace MarketPlace.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;

        public DbSet<ShipingAddress> ShipingAddresses { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        public DbSet<ProductBuyer> ProductBuyers { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ProductBuyer>()
                .HasKey(pb => new { pb.BuyerId, pb.ProductId });

           builder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany()
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ShipingAddress>()
                .HasOne(sa => sa.User)
                .WithMany()
                .HasForeignKey(sa => sa.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Category>()
                .HasData(new Category { Id = 1, Name = "Electronics" },
                           new Category { Id = 2, Name = "Clothing" },
                           new Category { Id = 3, Name = "Furniture" },
                           new Category { Id = 4, Name = "Books" },
                           new Category { Id = 5, Name = "Toys" });

            base.OnModelCreating(builder);
        }
    }
}
