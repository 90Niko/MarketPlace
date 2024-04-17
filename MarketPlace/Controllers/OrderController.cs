using MarketPlace.Core.Models.OrderInfoDto;
using MarketPlace.Core.Models.Rating;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Security.Claims;

namespace MarketPlace.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext data;

        public OrderController(ApplicationDbContext data)
        {
            this.data = data;
        }

        [HttpGet]
        public async Task<IActionResult> OrderInfo()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var order = await data.Orders
                 .Where(o => o.BuyerId == userId)
                 .Select(o => new OrderInfoModel()
                 {
                     BuyerId = o.BuyerId,
                     Date = o.OrderDate.ToString("dd/MM/yyyy/HH:mm"),
                     ProductName = o.ProductName,
                     ProductId = o.ProductId,
                     Address = o.ShipingAddress,
                     SellerName = o.SellerId,
                     Id = o.Id
                 }).ToListAsync();

            if (order == null)
            {
                return BadRequest();
            }

            return View(order);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await data.Orders.FindAsync(id);

            if (order == null)
            {
                return BadRequest();
            }

            var allProducts = await data.Products.ToListAsync();

            string[] productId = order.ProductId.Split(',');

            if (productId.Length == 0)
            {
                return BadRequest();
            }

            List<OrderDetailsModel> products = new List<OrderDetailsModel>();

            if (productId.Length > 0)
            {
                int index = productId.Length;
                int i = 0;
                while (index > 0)
                {
                    var product = allProducts
                   .Where(p => p.Id == int.Parse(productId[i]))
                   .Select(p => new OrderDetailsModel()
                   {
                       DateOfOrder = order.OrderDate.ToString("dd/MM/yyyy/HH:mm"),
                       ImageUrl = p.Image,
                       ProdctName = p.Name,
                       ShipingAddress = order.ShipingAddress,
                       ProductId = p.Id

                   }).ToList();

                    if (product == null)
                    {
                        return BadRequest();
                    }

                    index--;
                    i++;
                    products.AddRange(product);
                }
                return View(products);
            }
            return RedirectToAction(nameof(OrderInfo));
        }

        public async Task<IActionResult> Rating(int id)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await data.Products.FirstAsync(p => p.Id == id);

            if (product == null)
            {
                return BadRequest();
            }

            var model = new RatingModel
            {
                ProductId = id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Rating(int id, RatingModel model)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var product = await data.Products.FindAsync(id);

            if (product == null)
            {
                return BadRequest();
            }

            var newRating = new ProductRating
            {
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                ProductId = id
            };

            await data.ProductRatings.AddAsync(newRating);
            await data.SaveChangesAsync();

            return RedirectToAction(nameof(Details));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
