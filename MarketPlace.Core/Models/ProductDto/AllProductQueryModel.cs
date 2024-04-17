using MarketPlace.Core.Enumaration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models.ProductDto
{
    public class AllProductQueryModel
    {
        public  int ProductsPerPage = 3;

        public string Category { get; set; } = null!;

        [Display(Name = "Search by name")]
        public string SearchTerm { get; set; } = null!;

        public ProductSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalProductsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = null!;

        public IEnumerable<AllProductsModel> Products { get; set; } = new List<AllProductsModel>();
    }
}
