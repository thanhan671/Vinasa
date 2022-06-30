using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vinasa.Validation
{
    public class StringRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (value != null && !string.IsNullOrEmpty(value.ToString()));
        }
    }

    public class DateTimeRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime >= DateTime.Now;
        }
    }

    public class DropDownListRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            int.TryParse(value.ToString(), out int iValue);
            return iValue > 0;
        }
    }
}