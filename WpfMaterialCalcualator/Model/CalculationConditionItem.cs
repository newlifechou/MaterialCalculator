using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Model
{
    /// <summary>
    /// 实体类-计算条件表项目
    /// </summary>
    public class CalculationConditionItem
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public double MoleWeight { get; set; }
        public double At { get; set; }
    }
}
