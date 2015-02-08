using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace WebApplication1.Controllers
{
    public class CustomersController:Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            using (var context=new MyContext())
            {
                var customers = context.Customers.ToList();
                ViewBag.Customers = customers;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Add(Customer customer)
        {
            using (var context=new MyContext())
            {
                var customers = context.Customers.ToList();
                ViewBag.Customers = customers;
                var customerWithSameNationalCode = context.Customers.SingleOrDefault(x => x.NationalId == customer.NationalId);
                context.Customers.Add(customer);
                context.SaveChanges();
            }
            return RedirectToAction("Add");
        }
    }
}