using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalcualator.Model;

namespace WpfMaterialCalcualator.Service
{
    public class MainDataService : IMainDataService
    {
        public void CalculateWt(ICollection<CalculationConditionItem> conditions, ICollection<CalculationResultItem> results)
        {
            if (conditions != null && results != null)
            {
                //计算前将results集合清空
                results.Clear();

                //得到所有组的总tmp和
                double sumAllTmp = conditions.Sum(c => c.MoleWeight * c.At);

                var query = from c in conditions
                            group c by c.GroupName into g
                            select new
                            {
                                GroupName = g.Key,
                                GroupAtSum = g.Sum(c => c.At),
                                GroupAtMoleSum = g.Sum(c => c.MoleWeight * c.At),
                                GroupCount = g.Count(),
                                g
                            };

                foreach (var item in query)
                {
                    CalculationResultItem tmpCalculationResultItem = new CalculationResultItem();
                    tmpCalculationResultItem.GroupName = item.GroupName;
                    //按照组类生成名称和临时值,分两种情况，一个一组，和多个一组
                    StringBuilder sb = new StringBuilder();
                    if (item.GroupCount == 1)
                    {
                        sb.Append(item.g.FirstOrDefault().MaterialName);
                    }
                    else
                    {
                        foreach (var c in item.g)
                        {
                            sb.Append(c.MaterialName);
                            //保留两位小数
                            sb.Append((c.At / item.GroupAtSum * 100).ToString("N2"));
                        }
                    }

                    tmpCalculationResultItem.GroupComposition = sb.ToString();

                    tmpCalculationResultItem.Wt = item.GroupAtMoleSum / sumAllTmp * 100;
                    results.Add(tmpCalculationResultItem);
                }
            }
        }

        public void ClearResultWeigtht(ICollection<CalculationResultItem> results)
        {
            foreach (var item in results)
            {
                item.Wt = 0;
            }
        }
    }
}
