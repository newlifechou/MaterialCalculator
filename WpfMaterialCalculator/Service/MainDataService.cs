using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMaterialCalculator.CommonHelper;
using WpfMaterialCalculator.Model;
using System.Data.SQLite;
using System.Text.RegularExpressions;

namespace WpfMaterialCalculator.Service
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


        public bool ClearConditions()
        {
            string cmdText = "delete from tempCondition";
            return SqliteHelper.ExecuteNonQuery(cmdText, null) > 0;
        }

        public bool AddConditions(IList<CalculationConditionItem> items)
        {
            ClearConditions();
            (items as List<CalculationConditionItem>).ForEach(item =>
            {
                AddCondition(item);
            });
            return true;
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
                            //该组分名称当中是否包含多个元素，也就是说该组分是否是化合物形式存在
                            //判断方法就是大写字母的数量是否大于等于2
                            //如果是就加括号
                            if (CheckCapitalLetterMoreThanOne(c.MaterialName))
                            {
                                sb.Append("(");
                                sb.Append(c.MaterialName);
                                sb.Append(")");
                            }
                            else
                            {
                                sb.Append(c.MaterialName);
                            }
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
        /// <summary>
        /// 判断输入字符串当中是否包含大于一个的大写字母
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private bool CheckCapitalLetterMoreThanOne(string text)
        {
            string regexPattern = @"^[A-Z]{1}\w*[A-Z]{1}\w*$";
            return Regex.IsMatch(text, regexPattern);
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
            if (totalWeight > 0)
            {
                foreach (var item in results)
                {
                    item.Weight = item.Wt * totalWeight / 100;
                }
            }
        }

        public void CalcualteWithOneGroupWeight(CalculationResultItem alreadyKnownGroup, double groupWeight, ICollection<CalculationResultItem> results,
            out double totalWeight)
        {
            if (groupWeight > 0)
            {
                totalWeight = groupWeight / (alreadyKnownGroup.Wt / 100);
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
            List<ProjectItem> results = new List<ProjectItem>();
            string cmdText = "select id,projectname,savedate from project order by savedate desc ";
            SQLiteDataReader sdr = SqliteHelper.ExecuteReader(cmdText, null);
            while (sdr.Read())
            {
                ProjectItem tmp = new ProjectItem();
                tmp.ProjectId = sdr.GetGuid(0);
                tmp.ProjectName = sdr.GetString(1);
                tmp.SaveDate = sdr.GetDateTime(2);
                results.Add(tmp);
            }
            sdr.Close();
            return results;
        }

        public bool AddProject(ProjectItem item)
        {
            string cmdText = "insert into project (id,projectname,savedate) values (@id,@projectname,@savedate)";
            SQLiteParameter[] cmdParameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@id",item.ProjectId),
                new SQLiteParameter("@projectname",item.ProjectName),
                new SQLiteParameter("@savedate",item.SaveDate)
            };
            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }

        public bool DeleteProject(ProjectItem item)
        {
            string cmdText = "delete from project where id=@id";
            SQLiteParameter[] cmdParameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@id",item.ProjectId),
            };
            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;

        }

        public List<CalculationConditionItem> GetCalculationsByProjectId(Guid projectId)
        {
            List<CalculationConditionItem> results = new List<CalculationConditionItem>();
            string cmdText = "select id,groupname,materialname,moleweight,at from projectitem where projectid=@projectid order by groupname";
            SQLiteParameter[] cmdParameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@projectid",projectId)
            };
            SQLiteDataReader dr = SqliteHelper.ExecuteReader(cmdText, cmdParameters);
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

        public bool AddConditionsByProjectId(IList<CalculationConditionItem> conditions, Guid projectId)
        {
            foreach (var item in conditions)
            {
                AddSingleConditionByProjectId(item, projectId);
            }
            return true;
        }

        public bool AddSingleConditionByProjectId(CalculationConditionItem item,Guid projectId)
        {
            string cmdText = "insert into projectitem (id,groupname,materialName,moleWeight,at,projectid) values  (@id,@groupname,@materialName,@moleWeight,@at,@projectid) ";
            SQLiteParameter[] cmdParameters =
            {
                new SQLiteParameter("@id",Guid.NewGuid()),
                new SQLiteParameter("@groupname",item.GroupName),
                new SQLiteParameter("@materialName",item.MaterialName),
                new SQLiteParameter("@moleWeight",item.MoleWeight),
                new SQLiteParameter("@at",item.At),
                new SQLiteParameter("@projectid",projectId)
            };

            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }
        public bool DeleteConditionsByProjectId(Guid projectId)
        {
            string cmdText = "delete from projectitem where projectid=@projectid";
            SQLiteParameter[] cmdParameters = new SQLiteParameter[]
            {
                new SQLiteParameter("@projectid",projectId)
            };
            return SqliteHelper.ExecuteNonQuery(cmdText, cmdParameters) > 0;
        }

        public void CalcualteElementWeight(ICollection<CalculationConditionItem> conditions, double totalWeight, ICollection<ElementResultItem> results)
        {
            if (conditions!=null&&totalWeight>0&&results!=null)
            {
                var SumAllTmp = conditions.Sum(c => c.MoleWeight * c.At);
                foreach (var c in conditions)
                {
                    var result = new ElementResultItem();
                    result.ElementName = c.MaterialName;
                    result.Weight = totalWeight * c.MoleWeight * c.At / SumAllTmp;

                    results.Add(result);
                }
            }
        }
    }
}
