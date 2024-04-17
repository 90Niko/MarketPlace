using MarketPlace.Core.Contracts.IOrderService;
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
        private readonly IOrderService orderService;

        public OrderController(ApplicationDbContext _data,IOrderService _orderService)
        {
            data = _data;
            orderService = _orderService;
        }

        [HttpGet]
        public async Task<IActionResult> OrderInfo()
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

           var order= await orderService.AllOrdersAsync(userId);

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

           var model = await orderService.RatingAsync(id);

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

           var result = await orderService.RatingResultAsync(id, model);

            return RedirectToAction(nameof(OrderInfo));
        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
