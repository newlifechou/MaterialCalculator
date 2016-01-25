using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Model
{
    /// <summary>
    /// 实体类-计算结果表项目
    /// </summary>
    public class CalculationResultItem:NotifyPropertyChangedBase
    {

        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string GroupComposition { get; set; }
        //public double Tmp { get; set; }
        public double Wt { get; set; }

        /// <summary>
        /// 暂时只设置Weight属性的自动通知，用于MainView上的重量计算
        /// </summary>
        private double weight;
        public double Weight
        {
            get { return weight; }
            set { weight = value;RaisePropertyChanged("Weight"); }
        }

    }
}
