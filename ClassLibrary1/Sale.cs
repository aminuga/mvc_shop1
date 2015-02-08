using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Sale
    {
        public int SaleId { get; set; }
        public string Item { get; set; }
        public float UnitPrice { get; set; }
        public float Weight { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public DateTime DateTime { get; set; }
    }
}
