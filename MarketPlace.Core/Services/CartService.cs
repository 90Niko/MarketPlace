using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Core.Contracts.ICartService;
using MarketPlace.Core.Models.CartDto;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Core.Services
{
    public class CartService : ICartService
    {
        private readonly ApplicationDbContext data;

        public CartService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public async Task<IEnumerable<CartFormModel>> AllCartAsync(string buyer)
        {
            return await data.ProductBuyers
                   .Where(p => p.BuyerId == buyer)
                   .Select(p => new CartFormModel()
                   {
                       Id = p.Product.Id,
                       ProductDescription = p.Product.Description,
                       ProductName = p.Product.Name,
                       ProductPrice = p.Product.Price,
                       Seller = p.Product.Seller.UserName,
                       CartQuantity = p.Product.CartQuantity,
                       Category = p.Product.Category.Name
                   }
                    ).ToListAsync();
        }

        public Task FindAddressByUserId(string userId)
        {
            return data.ShipingAddresses
                .Where(p => p.UserId == userId)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
        }

        public Task<int> FindProductById(int id)
        {
            return data.Products
                .Where(p => p.Id == id)
                .Select(p => p.Id)
                .FirstOrDefaultAsync();
        }
    }
}
