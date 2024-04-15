using MarketPlace.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models.OrderInfoDto
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }  
        public string ImageUrl { get; set; } = string.Empty;
        public string ProdctName { get; set; } = string.Empty;

        public string DateOfOrder { get; set; } = string.Empty;

        public string ShipingAddress { get; set; } = string.Empty;

    }
}
