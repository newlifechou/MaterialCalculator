using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Model
{
    /// <summary>
    /// 实体类-项目名称表
    /// </summary>
    public class ProjectItem
    {
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime SaveDate { get; set; }
    }
}
