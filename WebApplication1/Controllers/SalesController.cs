using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Domain;

namespace WebApplication1.Controllers
{
    public class SalesController:Controller
    {
        public ActionResult Report()
        {
            using (var context=new MyContext())
            {
                var items = context.Sales.Include(x => x.Customer).ToList();
                return View(items);
            }
        }

        [HttpGet]
        public ActionResult Add()
        {
            using (var context = new MyContext())
            {
                var sales = context.Sales.ToList();
                ViewBag.Customers = context.Customers.ToList();
                return View();
            }
        }

        [HttpPost]
        public ActionResult Add(string Item, int Weight, int CustomerId)
        {
            using (var context=new MyContext())
            {
                var item = context.Sales.Create();
                item.Item = Item;
                item.Weight = Weight;
                item.CustomerId = CustomerId;
                item.DateTime = DateTime.Now;
                context.Sales.Add(item);
                context.SaveChanges();
            }
            return RedirectToAction("Report");
        }
    }
}