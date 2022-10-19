using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Common.Products
{
    public class ProcessorProductEntity : ProductEntity
    {
        public ProcessorSpecifications Specifications { get; set; }
    }

    public class ProcessorSpecifications
    {
        public int Cores { get; set; }
        public int Threads { get; set; }
        public string Socket { get; set; }
    }
}