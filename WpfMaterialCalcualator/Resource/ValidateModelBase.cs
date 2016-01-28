using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Resource
{
    /// <summary>
    /// 这个是验证所有模型的基类
    /// </summary>
    public class ValidateModelBase:INotifyDataErrorInfo
    {
        public void ValidateProperty(string value,string propertyName,CustomValidationItem validtion)
        {
            List<string> errors = new List<string>();
            if (validtion.validationExpression(value))
            {
                errors.Add(validtion.ErrorMessage);
                errorList[propertyName] = errors;
                //考虑精简这个语句
                RaiseErrorsChanged(propertyName);
            }
            else if (errorList.ContainsKey(propertyName))
            {
                errorList.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }
        /// <summary>
        /// 验证方法
        /// 继承子类当中可以调用这个方法，传入一个自定义的验证列表，进行多种单值验证
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <param name="validations"></param>
        public void ValidateProperty(string value, string propertyName, List<CustomValidationItem> validations)
        {
            bool isValid = true;
            //循环遍历每一个验证规则，如果有问题，添加进errorlist当中
            //只要有一个规则没有通过，isValid就是false
            foreach (var item in validations)
            {
                List<string> errors = new List<string>();
                if (item.validationExpression(value))
                {
                    errors.Add(item.ErrorMessage);
                    isValid = false;
                    errorList[propertyName] = errors;                   
                }
            }
            //判断验证有没有通过
            if (isValid==false)
            {
                RaiseErrorsChanged(propertyName);
            }
            else if (errorList.ContainsKey(propertyName))
            {
                errorList.Remove(propertyName);
                RaiseErrorsChanged(propertyName);
            }
        }


        Dictionary<string, List<string>> errorList = new Dictionary<string, List<string>>();
        public IEnumerable GetErrors(string propertyName)
        {
            if (errorList.ContainsKey(propertyName))
            {
                return errorList[propertyName];
            }
            return null;
        }
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void RaiseErrorsChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public bool HasErrors
        {
            get
            {
                return errorList.Count > 0;
            }
        }
    }
}
