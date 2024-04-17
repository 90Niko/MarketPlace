using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models.ProductDto
{
    public class ProductQueryServiceModel
    {
        public int TotalProductsCount { get; set; }

        public IEnumerable<AllProductsModel> Products { get; set; }=new List<AllProductsModel>();
    }
}
