using MarketPlace.Core.Enumaration;
using MarketPlace.Core.Models.ProductDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Contracts
{
    public interface  IProductService
    {
        Task<ProductQueryServiceModel> AllAsync(
           string? category = null,
           string? searchTerm = null,
           ProductSorting sorting = ProductSorting.Newest,
           int currentPage = 1,
           int housesPerPage = 1);
    }
}
