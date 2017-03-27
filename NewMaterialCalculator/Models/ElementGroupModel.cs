using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMaterialCalculator.Models
{
    public class ElementGroupModel
    {
        public Guid ID { get; set; }
        public int GroupNumber { get; set; }
        public string GroupComposition { get; set; }
        public double Weight { get; set; }
    }
}
