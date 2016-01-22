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

                var query = from c in conditions
                            group c by c.GroupName into g
                            select g;

                foreach (var g in query)
                {
                    CalculationResultItem tmp = new CalculationResultItem();
                    tmp.GroupName = g.Key;

                    StringBuilder sb = new StringBuilder();
                    double tempValue = 0;
                    foreach (var c in g)
                    {
                        sb.Append(c.MaterialName);
                        sb.Append(c.At);
                        tempValue += c.MoleWeight * c.At;
                    }
                    tmp.GroupComposition = sb.ToString();
                    tmp.Tmp = tempValue;
                    results.Add(tmp);
                }

                double sumTmp = conditions.Sum(c => c.MoleWeight * c.At);
                foreach (var r in results)
                {
                    r.Wt= r.Tmp / sumTmp*100;
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
