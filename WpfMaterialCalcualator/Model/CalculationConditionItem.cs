using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalcualator.ValidationRule;

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
        [NotEmpty]
        public string MaterialName
        {
            get
            {
                return materialName;
            }
            set
            {
                materialName = value;
                //ValidateProperty(materialName, "MaterialName", ValidatonRuleFactory.NotEmpty());
            }
        }
        private double moleWeight;
        [Required(ErrorMessage ="Must Not Be Empty")]
        public double MoleWeight
        {
            get
            {
                return moleWeight;
            }
            set
            {
                moleWeight = value;
                //ValidateProperty(moleWeight.ToString(), "MoleWeight", ValidatonRuleFactory.NotEmptyAndMustPositive());
            }
        }
        public double At { get; set; }
    }
}
