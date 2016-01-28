using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Resource
{
    /// <summary>
    /// 自定义验证项目
    /// </summary>
    public class CustomValidationItem
    {
        /// <summary>
        /// 验证委托表达式
        /// </summary>
        public Func<string, bool> validationExpression { get; set; }
        /// <summary>
        /// 验证不通过的时候的错误消息
        /// </summary>
        public string ErrorMessage { get; set; }
    }
}
