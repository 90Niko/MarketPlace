using MarketPlace.Core.Enumaration;
using MarketPlace.Core.Models.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Contracts.IProductService
{
    public interface IProductService
    {
        Task<ProductQueryServiceModel> AllAsync(
           string? category = null,
           string? searchTerm = null,
           ProductSorting sorting = ProductSorting.Newest,
           int currentPage = 1,
           int housesPerPage = 1);

        Task<IEnumerable<ProductCategoryServiceModel>> AllCategoryAsync();

        //Task<ProductDetailsServiceModel> DetailsAsync(int id);

        Task DeleteAsync(int productId);

        Task<int> CreateAsync(ProductFormModel model, string sellerId);

        Task EditAsync(int id, ProductFormModel model);

        Task<ProductFormModel> GetProductByIdAsync(int id);

        Task<bool> ExistAsync(int id);
        Task<IEnumerable<AllProductsModel>> AllProductsUserIdAsync(string userId);
    }
}
