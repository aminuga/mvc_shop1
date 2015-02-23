using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Domain;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	public class SalesController : Controller
	{
		[Authorize(Roles="Admin")]
		public ActionResult Report()
		{
			using (var context = new MyContext())
			{
				ViewBag.Total = context.Sales.Sum(s => s.UnitPrice * s.Weight);
				var items = context.Sales.Include(x => x.Customer).Include(x => x.Item).ToList();
				return View(items);
			}
		}

		public string MyMethod(string password)
		{
			var sha1 = System.Security.Cryptography.SHA1.Create();
			var salt = "SomeRandomTExt";
			password += salt;
			var passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
			var hashButes = sha1.ComputeHash(passwordBytes);
			var hash = Convert.ToBase64String(hashButes);
			return hash;
		}

		public ActionResult CustomerDebt()
		{
			using (var context = new MyContext())
			{
				var debts = context.Sales.GroupBy(x => x.Customer).Select(g => new CustomerDebtModel
				{
					CustomerName = g.Key.Name,
					PurchaseCount = g.Count(),
					TotalWeight = g.Sum(x => x.Weight),
					TotalPrice = g.Sum(x => x.UnitPrice * x.Weight)
				}).ToList();
				return View(debts);
			}
		}

		public ActionResult CustomerDebtOld()
		{
			var context = new MyContext();
			List<CustomerDebtModel> debts;
			try
			{
				debts = context.Sales.GroupBy(x => x.Customer).Select(g => new CustomerDebtModel
				{
					CustomerName = g.Key.Name,
					PurchaseCount = g.Count(),
					TotalWeight = g.Sum(x => x.Weight),
					TotalPrice = g.Sum(x => x.UnitPrice * x.Weight)
				}).ToList();
			}
			finally
			{
				context.Dispose();
			}
			return View(debts);
		}

		public ActionResult ItemSale()
		{
			using (var context = new MyContext())
			{
				var items = context.Sales.GroupBy(x => x.Item).Select(g => new ItemSaleModel
				{
					ItemName = g.Key.Name,
					PurchaseCount = g.Count(),
					TotalWeight = g.Sum(x => x.Weight),
					TotalPrice = g.Sum(x => x.UnitPrice * x.Weight)
				}).ToList();
				return View(items);
			}
		}

		public double GetSum1(IEnumerable<Sale> sales)
		{
			var sum = 0.0;
			foreach (var x in sales)
			{
				sum += x.UnitPrice * x.Weight;
			}
			return sum;
		}

		public double GetSum2(IEnumerable<Sale> sales)
		{
			return sales.Sum(x => x.UnitPrice * x.Weight);
		}

		[HttpGet]
		public ActionResult Add()
		{
			using (var context = new MyContext())
			{
				var sales = context.Sales.ToList();
				ViewBag.Customers = context.Customers.ToList();
				ViewBag.Items = context.Items.ToList();
				return View();
			}
		}

		[HttpPost]
		public ActionResult Add(int ItemId, float UnitPrice, int Weight, int CustomerId)
		{
			using (var context = new MyContext())
			{
				var item = context.Sales.Create();
				item.ItemId = ItemId;
				item.Weight = Weight;
				item.UnitPrice = UnitPrice;
				item.CustomerId = CustomerId;
				item.DateTime = DateTime.Now;
				context.Sales.Add(item);
				context.SaveChanges();
			}
			return RedirectToAction("Report");
		}
	}
}