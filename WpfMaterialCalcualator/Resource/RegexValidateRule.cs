using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfMaterialCalcualator.Resource
{
    public class RegexValidateRule:ValidationRule
    {
        public string RegexExpression { get; set; }
        public string RegexMessage { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string ready = value.ToString();
            if (Regex.IsMatch(ready,RegexExpression))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, RegexMessage);
            }
        }
    }
}
