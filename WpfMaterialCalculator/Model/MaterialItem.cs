using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalculator.Model
{
    /// <summary>
    /// 实体类-材料库表项
    /// </summary>
    public class MaterialItem:ModelBase
    {
        public Guid Id { get; set; }
        [Required]
        public string MaterialName { get; set; }
        [Required]
        public double MoleWeight { get; set; }
        [Required]
        public int PopRate { get; set; }

    }
}
