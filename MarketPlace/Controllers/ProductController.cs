using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly ApplicationDbContext data;
        public ProductController(ApplicationDbContext data)
              : base(data)
        {
            this.data = data;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

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

            if (userId == null)
            {
                return Unauthorized();
            }

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

            await data.Products.AddAsync(newProduct);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> All()
        {
            string userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
   
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
        public async Task<IActionResult> Mine()
        {
            string userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

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

            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await data.Products.FirstOrDefaultAsync(p => p.Id == id && p.SellerId == userId);

            if (product == null)
            {
                return NotFound();
            }

            data.Products.Remove(product);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await data.Products.FirstOrDefaultAsync(p => p.Id == id && p.SellerId == userId);

            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductFormModel
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Image = product.Image,
                CategoryId = product.CategoryId,
                Categories = GetCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductFormModel model)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await data.Products.FirstOrDefaultAsync(p => p.Id == id && p.SellerId == userId);

            if (product == null)
            {
                return NotFound();
            }

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;
            product.Image = model.Image;
            product.CategoryId = model.CategoryId;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}
