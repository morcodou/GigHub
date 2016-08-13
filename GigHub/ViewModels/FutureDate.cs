using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace GigHub.ViewModels
{
    public class FutureDate : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime datetime;
            var isvalid = DateTime.TryParseExact(Convert.ToString(value),
                "d MMM yyyy",
                CultureInfo.CurrentCulture,
                DateTimeStyles.None, out datetime);

            //        DateTime.ParseExact("08 Feb 2011 13:46", "dd MMM yyyy HH:mm",
            //System.Globalization.CultureInfo.InvariantCulture);

            return isvalid && datetime > DateTime.Now;
        }
    }
}