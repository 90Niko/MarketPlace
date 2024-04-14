using MarketPlace.Core.Contracts;
using MarketPlace.Core.Enumaration;
using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext data;

        public ProductService(ApplicationDbContext data)
            => this.data = data;

        public async Task<ProductQueryServiceModel> AllAsync(string? category = null, string? searchTerm = null, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var query = this.data.Products.Where(q => q.IsApproved == true && q.Quantity > 0).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(category))
            {
                query = query.Where(p => p.Category.Name == category);
            }

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                                   (p.Name + " " + p.Description).ToLower().Contains(searchTerm.ToLower()));
            }

            query = sorting switch
            {
                ProductSorting.PriceLowToHigh => query.OrderBy(p => p.Price),
                ProductSorting.Newest => query.OrderByDescending(p => p.Id),
                _ => query.OrderByDescending(p => p.Id)
            };

            var totalProducts = query.Count();

            var products = query
                .Skip((currentPage - 1) * housesPerPage)
                .Take(housesPerPage)
                .Include(p => p.productRatings)
                .Select(p => new AllProductsModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.Category.Name,
                    Quantity = p.Quantity,
                    Seller = p.Seller.UserName,
                    CreatedOn = p.CreatedOn.ToString("dd/MM/yyyy"),
                    Image = p.Image,
                    Rating = p.productRatings.Count > 0 ? (int)p.productRatings.Average(r => r.Rating) : 0

                })
                .ToList();

            int totalProduct = await query.CountAsync();

            return new ProductQueryServiceModel
            {
                TotalProductsCount = totalProducts,
                Products = products
            };
        }
    }
}
