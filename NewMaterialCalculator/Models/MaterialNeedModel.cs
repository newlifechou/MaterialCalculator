using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMaterialCalculator.Models
{
    public class MaterialNeedModel
    {
        public Guid ID { get; set; }
        public double Density { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public int Quantity { get; set; }
        public double WeightLoss { get; set; }
        public double Weight { get; set; }
    }
}
