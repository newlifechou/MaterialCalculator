using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewMaterialCalculator.Models
{
    public class StandardMoldFill
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
