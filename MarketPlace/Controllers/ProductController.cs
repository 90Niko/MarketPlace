using MarketPlace.Core.Contracts;
using MarketPlace.Core.Contracts.IProductService;
using MarketPlace.Core.Models;
using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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
                Categories = await productService.AllCategoryAsync()
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
                model.Categories = await productService.AllCategoryAsync();

                return View(model);
            }

            await productService.CreateAsync(model, userId);

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

            IEnumerable<AllProductsModel> products = await productService.AllProductsUserIdAsync(userId);

            if (userId == null)
            {
                return Unauthorized();
            }

            // await productService.AllProductsUserIdAsync(userId);

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

            await productService.DeleteAsync(id);

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

            if (!await productService.ExistAsync(id))
            {
                return BadRequest();
            }

            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductFormModel model)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await productService.AllCategoryAsync();

                return View(model);
            }

            var product = await productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await productService.EditAsync(id, model);

            return RedirectToAction(nameof(Mine));
        }
    }
}
