using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Core.Models.CartDto;
using MarketPlace.Infrastructure.Data.Models;

namespace MarketPlace.Core.Contracts.ICartService
{
    public interface ICartService
    {
        Task<IEnumerable<CartFormModel>> AllCartAsync(string buyer);

        Task <int> FindProductById(int id);

        Task FindAddressByUserId(string userId);    
    }
}
