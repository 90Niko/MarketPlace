using MarketPlace.Core.Models.CartDto;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Controllers
{
    [Authorize]
    public class CartController : BaseController
    {
        private readonly ApplicationDbContext data;
        public CartController(ApplicationDbContext data)
              : base(data)
        {
            this.data = data;
        }

        public async Task<IActionResult> Cart()
        {
            var userId = GetUserId();

            var productBuyer = await data.ProductBuyers
                .Where(p => p.BuyerId == userId)
               .Select(p => new CartFormModel()
               {
                   ProductId = p.Product.Id,
                   ProductDescription = p.Product.Description,
                   ProductName = p.Product.Name,
                   ProductPrice = p.Product.Price,
                   Seller = p.Product.Seller.UserName,
                   Category = p.Product.Category.Name
               }
               ).ToListAsync();

            return View(productBuyer);
        }

        public async Task<IActionResult> AddTocart(int id)
        {
            var productToAdd = await data.Products.FindAsync(id);

            if (productToAdd == null)
            {
                return BadRequest();
            }

            string userId = GetUserId();

            var productBuyer = new ProductBuyer
            {
                ProductId = productToAdd.Id,
                BuyerId = userId,
            };

            data.ProductBuyers.Add(productBuyer);
            data.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }

        public async Task<IActionResult> Buy(int id)
        {
            var productToBuy = await data.ProductBuyers
                .Where(p => p.ProductId == id && p.BuyerId == GetUserId())
                .FirstOrDefaultAsync();

            if (productToBuy == null)
            {
                return BadRequest();
            }

            data.ProductBuyers.Remove(productToBuy);
            data.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }

        [HttpGet]
        public async Task<IActionResult> RemoveFromCart(CartFormModel model)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }
            var productToRemove = await data.ProductBuyers
                .Where(p => p.ProductId == model.ProductId && p.BuyerId == userId)
                .FirstOrDefaultAsync();

            return View(productToRemove);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var productToRemove = await data.ProductBuyers
                .Where(p => p.ProductId == id && p.BuyerId == GetUserId())
                .FirstOrDefaultAsync();

            if (productToRemove == null)
            {
                return BadRequest();
            }

            data.ProductBuyers.Remove(productToRemove);
            data.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }
    }
}
