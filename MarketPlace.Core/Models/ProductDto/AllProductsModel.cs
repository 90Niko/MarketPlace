using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models.ProductDto
{
    public class AllProductsModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string Image { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;

        public string Seller { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Quantity { get; set; } = null!;

        public int Rating { get; set; }
    }
}
