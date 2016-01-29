using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace WpfMaterialCalcualator.ValidationRule
{
    public class CustomValidation : ICustomValidation
    {
        public bool IsEmail(string text)
        {
            string pattern = @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
            return IsRegexMatch(text, pattern);
        }

        public bool IsNotEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public bool IsPositiveNumber(string text)
        {
            string pattern=@"^[1-9]\d*\.?\d*$";
            return IsRegexMatch(text,pattern);
        }

        public bool IsRegexMatch(string text, string regexExpression)
        {
            return Regex.IsMatch(text, regexExpression);
        }
    }
}
