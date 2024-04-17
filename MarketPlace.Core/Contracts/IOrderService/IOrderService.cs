using MarketPlace.Core.Models.OrderInfoDto;
using MarketPlace.Core.Models.Rating;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Core.Contracts.IOrderService
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderInfoModel>> AllOrdersAsync(string userId);

        Task <OrderDetailsModel> DetailsAsync(int orderId);

        Task <RatingModel> RatingAsync(int productId);

        Task <RatingModel> RatingResultAsync(int productId, RatingModel model);
    }
}
