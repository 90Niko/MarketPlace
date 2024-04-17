using MarketPlace.Controllers;
using MarketPlace.Core.Contracts.IOrderService;
using MarketPlace.Core.Models.OrderInfoDto;
using MarketPlace.Core.Models.Rating;
using MarketPlace.Infrastructure.Data.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.TestProject
{
    [TestClass]
    public class OrderTest
    {
        [TestMethod]
        public void AllOrders_Method()
        {
            var order = new Order
            {
                BuyerId = "1",
                OrderDate = DateTime.Now,
                ProductName = "IPhone",
                ProductId = "1",
                ShipingAddress = "Test",
                SellerId = "2",
                Id = 1
            };

            var orderService = new Mock<IOrderService>();

            orderService.Setup(o => o.AllOrdersAsync("1"))
                .ReturnsAsync(new List<OrderInfoModel>
                {
                    new OrderInfoModel
                    {
                        BuyerId = "1",
                        Date = DateTime.Now.ToString("dd/MM/yyyy/HH:mm"),
                        ProductName = "IPhone",
                        ProductId = "1",
                        Address = "Test",
                        SellerName = "2",
                        Id = 1
                    }
                });

            var controller = new OrderController(null, orderService.Object);

            var result = controller.OrderInfo();

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Details_Method()
        {
            var order = new Order
            {
                BuyerId = "1",
                OrderDate = DateTime.Now,
                ProductName = "IPhone",
                ProductId = "1",
                ShipingAddress = "Test",
                SellerId = "2",
                Id = 1
            };

            var orderService = new Mock<IOrderService>();

            orderService.Setup(o => o.DetailsAsync(1))
                .ReturnsAsync(new OrderDetailsModel
                {
                  Id = 1,
                  ProductId = 1,
                  ImageUrl = "Test",
                  ProdctName = "IPhone",
                  DateOfOrder = DateTime.Now.ToString("dd/MM/yyyy/HH:mm"),
                  ShipingAddress = "Test"
                });

            var controller = new OrderController(null, orderService.Object);

            var result = controller.Details(1);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Rating_Method()
        {
            var orderService = new Mock<IOrderService>();

            orderService.Setup(o => o.RatingAsync(1))
                .ReturnsAsync(new RatingModel
                {
                    ProductId = 1,
                    Rating = 5
                });

            var controller = new OrderController(null, orderService.Object);

            var result = controller.Rating(1);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void RatingResult_Method()
        {
            var orderService = new Mock<IOrderService>();

            orderService.Setup(o => o.RatingResultAsync(1, new RatingModel
            {
                ProductId = 1,
                Rating = 5
            }))
                .ReturnsAsync(new RatingModel
                {
                    ProductId = 1,
                    Rating = 5
                });

            var controller = new OrderController(null, orderService.Object);

            var result = controller.Rating(1, new RatingModel
            {
                ProductId = 1,
                Rating = 5
            });

            Assert.IsNotNull(result);
        }
    }
}
