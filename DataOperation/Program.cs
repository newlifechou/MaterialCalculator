using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataOperation.BasicDataServices;
using System.Xml;
using System.Xml.Linq;

namespace DataOperation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var service=new ElementServiceClient())
            {
                string datafile = System.IO.Path.Combine(Environment.CurrentDirectory, "Data", "SubstanceData.xml");
                XDocument document = XDocument.Load(datafile);
                //var query = from element in document.Descendants("Substance")
                //            orderby int.Parse(element.Attribute("ANumber").Value)
                //            select new DcBDElement()
                //            {
                //                ID = Guid.NewGuid(),
                //                Name = element.Attribute("ElementName").Value,
                //                AtomicNumber = int.Parse(element.Attribute("ANumber").Value),
                //                MolWeight = double.Parse(element.Element("MolWeight").Value),
                //                Density = double.Parse(element.Element("Density").Value),
                //                MeltingPoint=element.Element("MeltingPoint").Value,
                //                BoilingPoint=element.Element("BoilingPoint").Value
                //            };
                //query.ToList().ForEach(i =>
                //{
                //    service.AddElement(i);
                //});

                Console.WriteLine("Done");
                Console.Read();
            }
        }
    }
}
