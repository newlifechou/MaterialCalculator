using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMaterialCalculator.Models
{
    public class ElementModel
    {
        public Guid ID { get; set; }
        public int GroupNumber { get; set; }
        public string Name { get; set; }
        public double MolWeight { get; set; }
        public double At { get; set; }
        public double Wt { get; set; }
        public double Weight { get; set; }
    }
}
