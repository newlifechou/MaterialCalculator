using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalculator.Model
{
    /// <summary>
    /// 实体类-项目名称表
    /// </summary>
    public class ProjectItem:ModelBase
    {
        public Guid ProjectId { get; set; }
        [Required]
        public string ProjectName { get; set; }
        [Required]
        public DateTime SaveDate { get; set; }
    }
}
