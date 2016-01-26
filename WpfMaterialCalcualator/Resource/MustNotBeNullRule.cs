using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfMaterialCalcualator.Resource
{
    /// <summary>
    /// 验证不为空
    /// </summary>
    public class MustNotBeNullRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(true, null);
            }
            else
            {
                return new ValidationResult(false, "Can not be Null or Empty");
            }
        }
    }
}
