using MarketPlace.Core.Models.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Contracts.ProductContract
{
    public interface IProductServices
    {
        Task<IEnumerable<ProductCategoryServiceModel>> AllCategoriesAsync();

        Task<int> CreateAsync(ProductFormModel product,string sellerId);    
    }
}
