using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.ValidationRule
{
    /// <summary>
    /// 自定义检验类接口
    /// </summary>
    public interface ICustomValidation
    {
        bool IsNotEmpty(string text);
        bool IsPositiveNumber(string text);
        bool IsEmail(string text);

        bool IsRegexMatch(string text, string regexExpression);
    }
}
