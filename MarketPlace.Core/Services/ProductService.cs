using MarketPlace.Core.Contracts.IProductService;
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

        public ProductService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<ProductQueryServiceModel> AllAsync(string? category = null, string? searchTerm = null, ProductSorting sorting = ProductSorting.Newest, int currentPage = 1, int housesPerPage = 1)
        {
            var query = data.Products.Where(q => q.IsApproved == true && q.Quantity > 0).AsNoTracking();

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

        public async Task<IEnumerable<ProductCategoryServiceModel>> AllCategoryAsync()
        {
            return await data
                .Categories
                .Where(c => c.IsDeleted==false)
                .Select(c => new ProductCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<AllProductsModel>> AllProductsUserIdAsync(string userId)
        {
            return await data
                .Products
                .Where(p => p.SellerId == userId)
                .Select(p => new AllProductsModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    CreatedOn = p.CreatedOn.ToString("dd/MM/yyyy"),
                    Seller = p.Seller.UserName,
                    Category = p.Category.Name
                })
                .ToListAsync();
        }

        public Task<int> CreateAsync(ProductFormModel model, string sellerId)
        {
            return Task.Run(async () =>
            {
                var product = new Product
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    CategoryId = model.CategoryId,
                    SellerId = sellerId,
                    Image = model.Image,
                    CreatedOn = DateTime.UtcNow
                };

                await data.Products.AddAsync(product);
                await data.SaveChangesAsync();

                return product.Id;
            });
        }

        public async Task DeleteAsync(int productId)
        {
            {
                var product = await data.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (product == null)
                {
                    throw new InvalidOperationException("Product not found.");
                }

                data.Products.Remove(product);
                await data.SaveChangesAsync();
            }
        }

        public Task EditAsync(int productId, ProductFormModel model)
        {
            return Task.Run(async () =>
            {
                var product = await data.Products.FirstOrDefaultAsync(p => p.Id == productId);

                if (product == null)
                {
                    throw new InvalidOperationException("Product not found.");
                }

                product.Name = model.Name;
                product.Description = model.Description;
                product.Price = model.Price;
                product.Quantity = model.Quantity;
                product.CategoryId = model.CategoryId;
                product.Image = model.Image;

                await data.SaveChangesAsync();
            });
        }

        public Task<bool> ExistAsync(int id)
        {
            return data.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<ProductFormModel> GetProductByIdAsync(int id)
        {
            var product = await data.Products
                 .Where(p => p.Id == id)
                 .Select(p => new ProductFormModel
                 {
                     Name = p.Name,
                     Description = p.Description,
                     Price = p.Price,
                     Quantity = p.Quantity,
                     CategoryId = p.CategoryId,
                     Image = p.Image
                 })
                 .FirstOrDefaultAsync();

            if (product != null)
            {
                product.Categories = await AllCategoryAsync();

            }
 
            return product;
        }
    }
}
