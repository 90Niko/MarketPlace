using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Models.OrderInfoDto
{
    public class OrderInfoModel
    {
        public int Id { get; set; } 

        public string BuyerId { get; set; }=string.Empty;

        public string Date { get; set; } = string.Empty;

        public string ProductName { get; set; } = string.Empty;

        public string ProductId { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string SellerName { get; set; } = string.Empty;
    }
}
