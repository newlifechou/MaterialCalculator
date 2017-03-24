using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMaterialCalculator.Models
{
    public class StandardMaterialNeed
    {
        public Guid ID { get; set; }
        public string Composition { get; set; }
        public string Dimension { get; set; }
        public double Weight { get; set; }
        public string Remark { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
