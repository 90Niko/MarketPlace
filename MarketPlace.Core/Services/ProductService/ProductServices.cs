using MarketPlace.Core.Contracts.ProductContract;
using MarketPlace.Core.Models.ProductDto;
using MarketPlace.Infrastructure.Data.Comman;
using Microsoft.EntityFrameworkCore;

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
            return await _repository.AllReadOnly<ProductCategoryServiceModel>()
                .Select(c => new ProductCategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }

        public async Task CreateAsync(ProductFormModel model)
        {
            var product = new ProductFormModel
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                Image = model.Image,
                CreatedOn = DateTime.UtcNow,
                CategoryId = model.CategoryId
            };

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

        }
    }
}
