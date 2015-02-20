using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
	public class CustomerDebtModel
	{
        public string CustomerName { get; set; }
		public int PurchaseCount { get; set; }
		public float TotalWeight { get; set; }
		public float TotalPrice { get; set; }
	}
}