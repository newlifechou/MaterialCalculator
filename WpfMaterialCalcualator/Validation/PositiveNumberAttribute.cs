using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.ValidationRule
{
    /// <summary>
    /// 验证是否为正的数字
    /// </summary>
    public class PositiveNumberAttribute:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return base.IsValid(value, validationContext);
            //暂时利用异常来判断是否是数字，以后更换正则表达式
            try
            {
                if (value!=null)
                {
                    double number = Convert.ToDouble(value);
                    if (number<=0)
                    {
                        return new ValidationResult("this number must be positive");
                    }
                }
            }
            catch (Exception)
            {
                return new ValidationResult("It is not a number");
            }
            return ValidationResult.Success;
        }
    }
}
