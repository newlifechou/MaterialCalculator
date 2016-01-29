using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.ValidationRule
{
    public class NotEmpty:ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var str = value.ToString();
            if (string.IsNullOrEmpty(str))
            {
                return false;
            }
            return true;
        }
        public override string FormatErrorMessage(string name)
        {
            return "Can not be empty";
        }
    }
}
