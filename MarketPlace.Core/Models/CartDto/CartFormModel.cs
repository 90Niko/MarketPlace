using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models.CartDto
{
    public class CartFormModel
    {

        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public string ProductDescription { get; set; } = null!;

        public decimal ProductPrice { get; set; }

        public string Category { get; set; } = null!;

        public int CartQuantity { get; set; }

        public string Seller { get; set; } = null!;


    }

}
