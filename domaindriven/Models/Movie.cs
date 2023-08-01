using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domaindriven.Models
{
    public class Instrument
    {
        public int InstrumentId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string? Manufacturer { get; set; }
    }
}
