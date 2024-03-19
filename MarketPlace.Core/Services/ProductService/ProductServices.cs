using MarketPlace.Core.Contracts.ProductContract;
using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Infrastructure.Data.Comman;
using MarketPlace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace MarketPlace.Core.Services.ProductService
{
    public class ProductServices : IProductServices
    {
        private readonly IRepository _repository;

        public ProductServices(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductCategoryServiceModel>> AllCategoriesAsync()
        {
            return await _repository.AllReadOnly<Category>()
                .Select(c => new ProductCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async  Task<int>  CreateAsync(ProductFormModel model,string sellerId)
        {   
            var product = new Infrastructure.Data.Models.Product
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedOn = DateTime.UtcNow,
                SellerId =sellerId.ToString()
            };
    
            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

           return product.Id;
     
        }
    }
}
