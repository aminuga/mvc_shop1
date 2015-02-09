using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Domain
{
	public class MyContext : DbContext
	{
		public MyContext() :
			base("MyConnectionString")
		{
		}
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
	}
}
