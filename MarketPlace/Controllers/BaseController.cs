using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarketPlace.Controllers
{
    public class BaseController : Controller
    {

        ApplicationDbContext data;

        public BaseController(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IActionResult Index()
        {
            return View();
        }
        public string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }

        public IEnumerable<ProductCategoryServiceModel> GetCategories()
            => data
                .Categories
                .Select(t => new ProductCategoryServiceModel()
                {
                    Id = t.Id,
                    Name = t.Name
                });
    }
}
