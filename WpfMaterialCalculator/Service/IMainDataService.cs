using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalculator.Model;

namespace WpfMaterialCalculator.Service
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

        void CalculateWithTotalWeight(ICollection<CalculationResultItem> results, double TotalWeight);
        void CalcualteWithOneGroupWeight(CalculationResultItem alreadyKnownGroup, double groupWeight, ICollection<CalculationResultItem> results,out double TotalWeight);


        //临时保存计算项目
        IList<CalculationConditionItem> GetAllConditions();
        bool AddCondition(CalculationConditionItem item);
        bool UpdateCondition(CalculationConditionItem item);
        bool DeleteCondition(CalculationConditionItem item);
        bool ClearCondition();

        //项目相关

        List<ProjectItem> GetAllProjects();
        bool AddProject(ProjectItem item);
        bool DeleteProject(ProjectItem item);

        List<CalculationConditionItem> GetCalculationsByProjectId(Guid projectId);
        bool AddConditionsByProjectId(IList<CalculationConditionItem> conditions, Guid projectId);
        bool DeleteConditionsByProjectId(Guid projectId);
    }
}
