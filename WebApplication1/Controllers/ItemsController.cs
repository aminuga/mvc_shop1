using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;

namespace WebApplication1.Controllers
{
	public class ItemsController : Controller
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
		public ActionResult Add(Item item, HttpPostedFileBase imageFile)
		{
			using (var context = new MyContext())
			{
				var items = context.Items.ToList();
				ViewBag.Items = items;
				item.FileName = imageFile.FileName;
				context.Items.Add(item);
				context.SaveChanges();
				var saveFileName = Server.MapPath("~/Pictures/" + imageFile.FileName);
				imageFile.SaveAs(saveFileName);
			}
			return RedirectToAction("Add");
		}
	}
}