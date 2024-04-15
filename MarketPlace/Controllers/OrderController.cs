using MarketPlace.Core.Models.OrderInfoDto;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

            List<OrderInfoModel> order = await data.Orders
                .Where(o => o.Id == id)
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

            return View(order);

        }

        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
        }
    }
}
