using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMaterialCalculator.Model
{
    /// <summary>
    /// 用于单质计算项
    /// </summary>
    public class ElementResultItem:ModelBase
    {
        public Guid Id { get; set; }
        public string ElementName { get; set; }
        public double Weight { get; set; }
    }
}
