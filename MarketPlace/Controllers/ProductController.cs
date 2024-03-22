using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MarketPlace.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext data;

        public ProductController(ApplicationDbContext data)
            => this.data = data;

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ProductFormModel model = new ProductFormModel()
            {
                Categories = await data.Categories.Select(c => new ProductCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name

                }).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
        {
            var userId = GetUserId();
            var newProduct = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Image = model.Image,
                CreatedOn = DateTime.Now,
                SellerId = userId,
                CategoryId = model.CategoryId
            };

            data.Products.Add(newProduct);
            data.SaveChanges();

            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> All()
        {
            var products = await data
                .Products
                .Select(p => new AllProductsModel()
                {
                    Id = p.Id,
                    Description = p.Description,
                    Name = p.Name,
                    Price = (decimal)p.Price,
                    Image = p.Image,
                    CreatedOn = p.CreatedOn.ToString("dd/MM/yyyy"),
                    Seller = p.Seller.UserName,
                    Category = p.Category.Name
                })
               .ToListAsync();

            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetUserId();
            var product = await data.Products.FirstOrDefaultAsync(p => p.Id == id && p.SellerId == userId);

            if (product == null)
            {
                return NotFound();
            }

            data.Products.Remove(product);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        private IEnumerable<ProductCategoryServiceModel> GetCategories()
            => data
                .Categories
                .Select(t => new ProductCategoryServiceModel()
                {
                    Id = t.Id,
                    Name = t.Name
                });

    }
}
