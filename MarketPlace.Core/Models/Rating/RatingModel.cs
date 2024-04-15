using MarketPlace.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MarketPlace.Infrastructure.Constants.DataConstants;

namespace MarketPlace.Core.Models.Rating
{
    public class RatingModel
    {

        public int Id { get; set; }
      
        [Range(ProductRatingMinValue, ProductRatingMaxValue)]
        public int Rating { get; set; }

        public string Comment { get; set; } = string.Empty;

        public string CreatedAt { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public int ProductId { get; set; }

        public ICollection<ProductRating> ProductRatings { get; set; } = new List<ProductRating>();

    }
}
