using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.DAL;

namespace Vinasa.Validation
{

    public class StringRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (value != null && !string.IsNullOrEmpty(value.ToString()));
        }
    }
}