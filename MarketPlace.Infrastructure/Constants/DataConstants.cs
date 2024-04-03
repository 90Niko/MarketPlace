using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Infrastructure.Constants
{
    public static class DataConstants
    {
        //Product Constants
        public const int ProductNameMinLength = 3;
        public const int ProductNameMaxLength = 50;

        public const int ProductDescriptionMinLength = 10;
        public const int ProductDescriptionMaxLength = 500;

        public const string ProductPriceMinimum = "0";
        public const string ProductPriceMaximum = "100000";

        //Date Format
        public const string DateFormat = "dd/MM/yyyy";

        public const string DateFormatWithTime = "dd/MM/yyyy HH:mm:ss";

        public const string DateFormatWithTimeAndZone = "dd/MM/yyyy HH:mm:ss zzz";

       //Category Constants
        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 50;

        //Shipping Address Constants
        public const int AddressStreetMinLength = 3;
        public const int AddressStreetMaxLength = 150;

        public const int AddressCityMinLength = 3;
        public const int AddressCityMaxLength = 50;

        public const int AddressCountryMinLength = 3;
        public const int AddressCountryMaxLength = 50;

        public const int AddressZipCodeMinLength = 0;
        public const int AddressZipCodeMaxLength = 4;

        public const string AddressZipCodeRegularExpression = "^d{4}$";

        //Product Rating Constants

        public const double ProductRatingMinValue = 1;
        public const double ProductRatingMaxValue = 5;

        public const int ProductCommentMinLength = 3;
        public const int ProductCommentMaxLength = 500;

    }
}
