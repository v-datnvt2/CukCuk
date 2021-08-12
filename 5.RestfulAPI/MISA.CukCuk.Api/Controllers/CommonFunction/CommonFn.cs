using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.CukCuk.Api.Controllers.CommonFunction
{
    public static class CommonFn
    {
        public static DateTime toCsharpDate(string dateString)
        {
            return DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        }
        public static bool isNumber(string numberString)
        {
            if (numberString=="")
            {
                return false;
            }
            bool isNumeric = int.TryParse("123", out _);
            return isNumeric;
        }
    }
}
