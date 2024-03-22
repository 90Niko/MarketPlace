using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models.CartDto
{
    public class CartFormModel : ProductDto.ProductFormModel
    {
        public int ProductId { get; set; }

        public string BuyerId { get; set; }   

        public ICollection<ProductDto.ProductFormModel> Products { get; set; } = new List<ProductDto.ProductFormModel>();
    }
}
