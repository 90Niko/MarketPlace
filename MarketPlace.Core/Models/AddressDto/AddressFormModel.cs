using MarketPlace.Core.Constants;
using MarketPlace.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Core.Models.AddressDto
{
    public class AddressFormModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(DataConstants.AddressStreetMaxLength, MinimumLength = DataConstants.AddressStreetMinLength, ErrorMessage = MessageConstants.LengthMessage)]
        public string Street { get; set; } = string.Empty;


        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(DataConstants.AddressCityMaxLength, MinimumLength = DataConstants.AddressCityMinLength, ErrorMessage = MessageConstants.LengthMessage)]
        public string City { get; set; } = string.Empty;


        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(DataConstants.AddressCountryMaxLength, MinimumLength = DataConstants.AddressCountryMinLength, ErrorMessage = MessageConstants.LengthMessage)]
        public string Country { get; set; } = string.Empty;


        [Required(ErrorMessage = MessageConstants.RequiredMessage)]
        [StringLength(DataConstants.AddressZipCodeMaxLength, MinimumLength = DataConstants.AddressZipCodeMinLength, ErrorMessage = MessageConstants.LengthMessage)]
        public string ZipCode { get; set; } = string.Empty;

        
        public string UserId { get; set; } = string.Empty;
    }
}
