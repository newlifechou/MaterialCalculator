using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalcualator.Model
{
    /// <summary>
    /// 实体类-材料库表项
    /// </summary>
    public class MaterialItem
    {
        public Guid Id { get; set; }
        public string MaterialName { get; set; }
        public double MoleWeight { get; set; }
        public int PopRate { get; set; }



    }
}
