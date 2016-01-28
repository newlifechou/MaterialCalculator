using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalcualator.Resource;

namespace WpfMaterialCalcualator.Model
{
    /// <summary>
    /// 实体类-计算条件表项目
    /// </summary>
    public class CalculationConditionItem : ValidateModelBase
    {
        public Guid Id { get; set; }

        private string groupName;
        public string GroupName
        {
            get
            {
                return groupName;
            }
            set
            {
                groupName = value;
            }
        }
        public string materialName;
        public string MaterialName
        {
            get
            {
                return materialName;
            }
            set
            {
                materialName = value;
                ValidateProperty(materialName, "MaterialName", ValidatonRuleFactory.NotEmpty());
            }
        }
        public double MoleWeight { get; set; }
        public double At { get; set; }
    }
}
