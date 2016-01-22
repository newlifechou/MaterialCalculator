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
    public class CalculationResultItem
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string GroupComposition { get; set; }
        //public double Tmp { get; set; }
        public double Wt { get; set; }
        public double Weight { get; set; }
    }
}
