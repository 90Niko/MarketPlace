
using MarketPlace.Controllers;
using MarketPlace.Core.Contracts.IProductService;
using MarketPlace.Core.Enumaration;
using MarketPlace.Core.Models.ProductDto;
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
    public class Test
    {
        [TestMethod]
        public void AllCategories_Method()
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

            var prodcutService = new Mock<IProductService>();

            prodcutService.Setup(p => p.AllCategoryAsync())
                .ReturnsAsync(new List<ProductCategoryServiceModel>
                {
                    new ProductCategoryServiceModel
                    {
                        Name = "Test"
                    }
                });

            var controller = new ProductController(null, prodcutService.Object);

            var result = controller.Add();

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Add_Method()
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

            var prodcutService = new Mock<IProductService>();

            prodcutService.Setup(p => p.CreateAsync(It.IsAny<ProductFormModel>(), It.IsAny<string>()))
                .ReturnsAsync(1);

            var controller = new ProductController(null, prodcutService.Object);

            var result = controller.Add(new ProductFormModel
            {
                Name = "IPhone",
                Description = "Brand New",
                CategoryId = 1,
                Price = 1000,
                Quantity = 1
            });

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void All_Method()
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

            var prodcutService = new Mock<IProductService>();

            prodcutService.Setup(p => p.AllAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<ProductSorting>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new ProductQueryServiceModel
                {
                    Products = new List<AllProductsModel>
                    {
                        new AllProductsModel
                        {
                            Name = "IPhone",
                            Description = "Brand New",
                            Category = "Test"
                        }
                    }
                });

            var controller = new ProductController(null, prodcutService.Object);

            var result = controller.All(new AllProductQueryModel());

            Assert.IsNotNull(result);

        }
        [TestMethod]

        public void Mine_Method()
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

            var prodcutService = new Mock<IProductService>();

            prodcutService.Setup(p => p.AllProductsUserIdAsync(It.IsAny<string>()))
                .ReturnsAsync(new List<AllProductsModel>
                {
                    new AllProductsModel
                    {
                        Name = "IPhone",
                        Description = "Brand New",
                        Category = "Test"
                    }
                });

            var controller = new ProductController(null, prodcutService.Object);

            var result = controller.Mine();

            Assert.IsNotNull(result);

        }
        [TestMethod]
        public void Delete_Method()
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

            var prodcutService = new Mock<IProductService>();

            prodcutService.Setup(p => p.DeleteAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);

            var controller = new ProductController(null, prodcutService.Object);

            var result = controller.Delete(1);

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void Edit_Method()
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

            var prodcutService = new Mock<IProductService>();

            prodcutService.Setup(p => p.ExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            prodcutService.Setup(p => p.GetProductByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new ProductFormModel
                {
                    Name = "IPhone",
                    Description = "Brand New",
                    CategoryId = 1,
                    Price = 1000,
                    Quantity = 1
                });

            var controller = new ProductController(null, prodcutService.Object);

            var result = controller.Edit(1);

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void Edit_Method_Post()
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

            var prodcutService = new Mock<IProductService>();

            prodcutService.Setup(p => p.ExistAsync(It.IsAny<int>()))
                .ReturnsAsync(true);

            prodcutService.Setup(p => p.EditAsync(It.IsAny<int>(), It.IsAny<ProductFormModel>()))
                .Returns(Task.CompletedTask);

            var controller = new ProductController(null, prodcutService.Object);

            var result = controller.Edit(1, new ProductFormModel
            {
                Name = "IPhone",
                Description = "Brand New",
                CategoryId = 1,
                Price = 1000,
                Quantity = 1
            });

            Assert.IsNotNull(result);
        }
    }
}
