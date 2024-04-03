using MarketPlace.Core.Models.AddressDto;
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
            string userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var productBuyer = await data.ProductBuyers
               .Where(p => p.BuyerId == userId)
               .Select(p => new CartFormModel()
               {
                   Id = p.Product.Id,
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

            if (productToAdd.SellerId == userId)
            {
              return BadHttpRequestException("You can't buy your own product!");
            }

          var checkProduct = await data.ProductBuyers
                .Where(p => p.ProductId == productToAdd.Id && p.BuyerId == userId)
                .FirstOrDefaultAsync();

            if (checkProduct != null)
            {
               return BadHttpRequestException("You already have this product in your cart!");
            }

            var productBuyer = new ProductBuyer
            {
                ProductId = productToAdd.Id,
                BuyerId = userId,
            };

            data.ProductBuyers.Add(productBuyer);
            data.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }

        private IActionResult BadHttpRequestException(string v)
        {
           
            var badRequest = new BadRequestObjectResult(v);

            return badRequest;
        }

        public async Task<IActionResult> Buy(string buyerId)
        {
            buyerId = GetUserId();
            List<ProductBuyer> products = await data.ProductBuyers
                 .Where(p => p.BuyerId == buyerId)
                 .ToListAsync();

            foreach (var product in products)
            {
                data.ProductBuyers.Remove(product);
            }

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
                .Where(p => p.ProductId == model.Id && p.BuyerId == userId)
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

        [HttpGet]
        public async Task<IActionResult> AddAddress()
        {
            AddressFormModel model = new AddressFormModel()
            {
                Products = await data.ProductBuyers.Where(p => p.BuyerId == GetUserId()).Select(p => p.Product).ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddAddress(AddressFormModel model)
        {
            string userId = GetUserId();

            if (userId == null)
            {
                return Unauthorized();
            }

            var address = new ShipingAddress
            {
                Id = model.Id,
                Recipient = model.Recipient,
                City = model.City,
                Country = model.Country,
                ZipCode = model.ZipCode,
                Street = model.Street,
                UserId = userId,
                Products = await data.ProductBuyers.Where(p => p.BuyerId == userId).Select(p => p.Product).ToListAsync()
            };

            if (address == null)
            {
                return BadRequest();
            }
            
            data.ShipingAddresses.Add(address);
            data.SaveChanges();

            return RedirectToAction(nameof(Cart));
        }
    }
}
