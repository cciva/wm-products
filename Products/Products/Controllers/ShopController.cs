using Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Filter(Category category)
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            return View();
        }
    }
}