using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Model
{
    /// <summary>
    /// 实体类-计算条件表项目
    /// </summary>
    public class CalculationConditionItem : ModelBase
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
        [Required]
        public string MaterialName
        {
            get
            {
                return materialName;
            }
            set
            {
                materialName = value;
            }
        }
        private double moleWeight;
        [Required]
        public double MoleWeight
        {
            get
            {
                return moleWeight;
            }
            set
            {
                moleWeight = value;
            }
        }
        [Required]
        public double At { get; set; }
    }
}
