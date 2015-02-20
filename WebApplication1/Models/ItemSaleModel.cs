using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class ItemSaleModel
    {
        public string ItemName { get; set; }
        public int PurchaseCount { get; set; }
        public float TotalWeight { get; set; }
        public float TotalPrice { get; set; }
    }
}