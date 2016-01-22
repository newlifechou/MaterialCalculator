using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalcualator.Model;

namespace WpfMaterialCalcualator.Service
{
    /// <summary>
    /// 数据服务接口
    /// </summary>
    public interface IMainDataService
    {
        /// <summary>
        /// 计算Wt和装料
        /// </summary>
        /// <param name="conditions"></param>
        /// <param name="results"></param>
        /// <param name="alreadyKnow"></param>
        /// <param name="calWeight"></param>
        void CalculateWt(ICollection<CalculationConditionItem> conditions, ICollection<CalculationResultItem> results);

        /// <summary>
        /// 清除重量
        /// </summary>
        /// <param name="results"></param>
        void ClearResultWeigtht(ICollection<CalculationResultItem> results);

        void CalculateWeight(string alreadyKnownItem, double alreadyKnownWeight, ICollection<CalculationResultItem> results, double TotalWeight);

        IList<CalculationConditionItem> GetAllConditions();
        bool AddCondition(CalculationConditionItem item);
        bool UpdateCondition(CalculationConditionItem item);
        bool DeleteCondition(CalculationConditionItem item);
        bool ClearCondition();
    }
}
