using MarketPlace.Core.Contracts.ProductContract;
using MarketPlace.Core.Models.ProductDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new ProductFormModel
            {
                Categories = await _productServices.AllCategoriesAsync()
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Add(ProductFormModel model)
        {
            if (!ModelState.IsValid)
            {
               model.Categories = await _productServices.AllCategoriesAsync();
                return View(model);
            }

            model.Categories = await _productServices.AllCategoriesAsync();

            return RedirectToAction("Index", "Home");
        }
    }
}
