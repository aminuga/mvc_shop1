using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace WebApplication1.Controllers
{
    public class ItemsController:Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            using (var context = new MyContext())
            {
                var items = context.Items.ToList();
                ViewBag.Items = items;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Add(Item item)
        {
            using (var context = new MyContext())
            {
                var items = context.Items.ToList();
                ViewBag.Items = items;
                context.Items.Add(item);
                context.SaveChanges();
            }
            return RedirectToAction("Add");
        }
    }
}