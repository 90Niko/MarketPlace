using MarketPlace.Core.Contracts.IOrderService;
using MarketPlace.Core.Models.OrderInfoDto;
using MarketPlace.Core.Models.Rating;
using MarketPlace.Infrastructure.Data;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext data;

        public OrderService(ApplicationDbContext _data)
        {
            data = _data;
        }
        public Task<IEnumerable<OrderInfoModel>> AllOrdersAsync(string userId)
        {

            return Task.Run(() =>
            {
                return data.Orders
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
                    }
                     ).AsEnumerable();
            });
        }

        public Task<OrderDetailsModel> DetailsAsync(int orderId)
        {
            var order = data.Orders.FirstOrDefault(o => o.Id == orderId);

            var allProducts = data.Products.ToList();

            string[] productId = order.ProductId.Split(',');

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
                       ProductId = p.Id,
                       Id = order.Id,

                   }).FirstOrDefault();

                    products.Add(product);
                    i++;
                    index--;
                }
            }
            return Task.FromResult(products.FirstOrDefault());

        }

        public Task<RatingModel> RatingAsync(int productId)
        {
            var product = data.Products.FirstOrDefault(p => p.Id == productId);


            var model = new RatingModel
            {
                ProductId = productId,
            };

            return Task.FromResult(model);
        }

        public Task<RatingModel> RatingResultAsync(int productId, RatingModel model)
        {
            return Task.Run(async () =>
            {
                var product = data.Products.FirstOrDefault(p => p.Id == productId);

                var rating = new ProductRating();

                rating.Rating = model.Rating;
                rating.Comment = model.Comment;
                rating.CreatedAt = DateTime.UtcNow;
                rating.UserId = model.UserId;
                rating.ProductId = productId;

                data.ProductRatings.Add(rating);
                data.SaveChanges();

                return model;
            });
        }
    }
}
