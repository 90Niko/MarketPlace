using MarketPlace.Core.Contracts;
using MarketPlace.Core.Models;
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
        private readonly IProductService productService;
        public ProductController(ApplicationDbContext data, IProductService productService)
              : base(data)
        {
            this.data = data;
            this.productService = productService;
        }

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

            if (userId == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await data.Categories.Select(c => new ProductCategoryServiceModel()
                {
                    Id = c.Id,
                    Name = c.Name

                }).ToListAsync();

                return View(model);
            }

            var newProduct = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Image = model.Image,
                CreatedOn = DateTime.Now,
                SellerId = userId,
                CategoryId = model.CategoryId,
                Quantity = model.Quantity
            };

            await data.Products.AddAsync(newProduct);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
        public async Task<IActionResult> All([FromQuery] AllProductQueryModel query)
        {
            var model = await productService.AllAsync(
                query.Category,
                query.SearchTerm,
                query.Sorting,
                query.CurrentPage,
                query.ProductsPerPage);

            query.TotalProductsCount = model.TotalProductsCount;
            query.Products = model.Products;
            query.Categories = await data.Categories.Select(c => c.Name).ToListAsync();

            return View(query);

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
                Quantity = product.Quantity,
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
            product.Quantity = model.Quantity;
            product.CategoryId = model.CategoryId;

            await data.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}
