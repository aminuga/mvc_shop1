using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Domain
{
	public class MyContext : DbContext
	{
		public static void ComputeMigrations()
		{
			using (var context = new MyContext())
			{
				var migratorConfig = new DbMigrationsConfiguration<MyContext>();
				migratorConfig.AutomaticMigrationsEnabled = true;
				migratorConfig.AutomaticMigrationDataLossAllowed = true;
				migratorConfig.TargetDatabase = new System.Data.Entity.Infrastructure.DbConnectionInfo(context.Database.Connection.ConnectionString, "System.Data.SqlClient");
				var dbMigrator = new DbMigrator(migratorConfig);
				dbMigrator.Update();
			}
		}

		public MyContext() :
			base("MyConnectionString")
		{
		}
		public DbSet<Sale> Sales { get; set; }
		public DbSet<Customer> Customers { get; set; }
        public DbSet<Item> Items { get; set; }
	}
}