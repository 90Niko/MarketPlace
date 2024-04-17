using MarketPlace.Controllers;
using MarketPlace.Core.Contracts.ICartService;
using MarketPlace.Core.Models.CartDto;
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
    public class CartTest
    {

        [TestMethod]
        public void AllCart_Method()
        {
            var cart= new ProductBuyer
            {
                Product = new Product
                {
                    Name = "IPhone",
                    Description = "Brand New",
                    Category = new Category
                    {
                        Name = "Test"
                    }
                }
            };

            var cartService = new Mock<ICartService>();

            cartService.Setup(p => p.AllCartAsync("Test"))
                .ReturnsAsync(new List<CartFormModel>
                {
                    new CartFormModel
                    {
                       ProductDescription = "Brand New",
                       ProductName = "IPhone",
                    }
                });
            var controller = new CartController(null, cartService.Object);

            var result = controller.Cart();
        }
        [TestMethod]
        
        public void AddToCart_Method()
        {
            var product = new Product
            {
                Name = "IPhone",
                Description = "Brand New",
                Category = new Category
                {
                    Name = "Test"
                }
            };

            var cartService = new Mock<ICartService>();

            cartService.Setup(p => p.FindProductById(1))
                .ReturnsAsync(1);

            var controller = new CartController(null, cartService.Object);

            var result = controller.Cart();
        }   
    }
}
