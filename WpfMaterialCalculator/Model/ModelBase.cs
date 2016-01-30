using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalculator.Model
{
    /// <summary>
    /// 作为所有Model类的基类
    /// 实现了 INotifyPropertyChanged和IDataErrorInfo接口
    /// 前者用于通知属性变化，后者用于属性的验证
    /// </summary>
    public class ModelBase : INotifyPropertyChanged,IDataErrorInfo
    {
       /// <summary>
       /// 这个属性用于控制Command的可用与否
       /// </summary>
        public bool IsValid
        {
            get
            {
                if (ErrorList.Count > 0)
                {
                    return false;
                }
                return true;
            }
        }

        //存储整个Model的所有属性的错误列表
        private Dictionary<string, List<ValidationResult>> ErrorList = new Dictionary<string, List<ValidationResult>>();
        private void Add(string key, List<ValidationResult> errors)
        {
            if (ErrorList.ContainsKey(key))
            {
                ErrorList[key] = errors;
            }
            else
            {
                ErrorList.Add(key, errors);
            }
        }
        private void Remove(string key)
        {
            if (ErrorList.ContainsKey(key))
            {
                ErrorList.Remove(key);
            }
        }

        public string this[string columnName]
        {
            get
            {
                var context = new ValidationContext(this, null, null);
                context.MemberName = columnName;
                List<ValidationResult> errors = new List<ValidationResult>();
                Validator.TryValidateProperty(this.GetType().GetProperty(columnName).GetValue(this), context, errors);
                if (errors != null && errors.Count > 0)
                {
                    string errorMsg = string.Join(Environment.NewLine, errors.Select(i => i.ErrorMessage).ToArray());
                    Add(columnName, errors);
                    return errorMsg;
                }
                Remove(columnName);
                return string.Empty;

            }
        }

        public string Error
        {
            get
            {
                return null;
            }
        }
       /// <summary>
       /// 属性变化的时候通知
       /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged!=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
