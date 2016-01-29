using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalcualator.CommonHelper;
using WpfMaterialCalcualator.Model;
using System.Data.SQLite;

namespace WpfMaterialCalcualator.Service
{
    public class MainDataService : IMainDataService
    {
        public IList<CalculationConditionItem> GetAllConditions()
        {
            IList<CalculationConditionItem> results = new List<CalculationConditionItem>();
            string cmdText = "select id,groupname,materialname,moleweight,at from tempcondition order by groupname";
            SQLiteDataReader dr = SqliteHelper.ExecuteReader(cmdText, null);
            while (dr.Read())
            {
                CalculationConditionItem tmp = new CalculationConditionItem();
                tmp.Id = dr.GetGuid(0);
                tmp.GroupName = dr.GetString(1);
                tmp.MaterialName = dr.GetString(2);
                tmp.MoleWeight = dr.GetDouble(3);
                tmp.At = dr.GetDouble(4);
                results.Add(tmp);
            }
            dr.Close();
            return results;
        }

        public bool AddCondition(CalculationConditionItem item)
        {
            string cmdText = "insert into tempcondition (id,groupname,materialName,moleWeight,at) values  (@id,@groupname,@materialName,@moleWeight,@at) ";
            SQLiteParameter[] cmdParameters =
            {
                new SQLiteParameter("@id",item.Id),
                new SQLiteParameter("@groupname",item.GroupName),
                new SQLiteParameter("@materialName",item.MaterialName),
                new SQLiteParameter("@moleWeight",item.MoleWeight),
                new SQLiteParameter("@at",item.At)
            };

            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }

        public bool UpdateCondition(CalculationConditionItem item)
        {
            string cmdText = "update tempcondition set groupname=@groupname,materialName=@materialName,moleWeight=@moleWeight,at=@at where id=@id";
            SQLiteParameter[] cmdParameters =
            {
                new SQLiteParameter("@groupname",item.GroupName),
                new SQLiteParameter("@materialName",item.MaterialName),
                new SQLiteParameter("@moleWeight",item.MoleWeight),
                new SQLiteParameter("@at",item.At),
                new SQLiteParameter("@id",item.Id)
            };

            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }

        public bool DeleteCondition(CalculationConditionItem item)
        {
            string cmdText = "delete from tempCondition where id=@id";
            SQLiteParameter[] cmdParameters =
            {
                new SQLiteParameter("@id",item.Id),
            };

            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }


        public bool ClearCondition()
        {
            string cmdText = "delete from tempCondition";
            return SqliteHelper.ExecuteNonQuery(cmdText, null) > 0;
        }


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
                    tmpCalculationResultItem.Id = Guid.NewGuid();
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
                    tmpCalculationResultItem.Weight = 0;
                    results.Add(tmpCalculationResultItem);
                }
            }
        }


        public void ClearResultWeigtht(ICollection<CalculationResultItem> results)
        {
            foreach (var item in results)
            {
                item.Weight = 0;
            }
        }

        public void CalculateWithTotalWeight(ICollection<CalculationResultItem> results, double totalWeight)
        {
            if (totalWeight>0)
            {
                foreach (var item in results)
                {
                    item.Weight = item.Wt * totalWeight / 100;
                }
            }
        }

        public void CalcualteWithOneGroupWeight(CalculationResultItem alreadyKnownGroup, double groupWeight, ICollection<CalculationResultItem> results,
            out  double totalWeight)
        {
            if (groupWeight>0)
            {
                totalWeight = groupWeight / (alreadyKnownGroup.Wt/100);
                //首先得知总重量，然后调用总重量已知的计算方法
                CalculateWithTotalWeight(results, totalWeight);
            }
            else
            {
                totalWeight = 0;
            }
        }

        public List<ProjectItem> GetAllProjects()
        {
            throw new NotImplementedException();
        }

        public bool AddProject(ProjectItem item)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProject(ProjectItem item)
        {
            throw new NotImplementedException();
        }

        public List<CalculationConditionItem> GetCalculationsByProjectId(Guid projectId)
        {
            throw new NotImplementedException();
        }

        public bool AddConditionsByProjectId(IList<CalculationConditionItem> conditions, Guid projectId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteConditionsByProjectId(Guid projectId)
        {
            throw new NotImplementedException();
        }
    }
}
