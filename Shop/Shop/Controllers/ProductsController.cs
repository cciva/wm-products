using Products.Resources;
using Products.Startup;
using Shop.Library.Model;
using Shop.Library.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        public ActionResult Browse()
        {
            IStore store = ShopApp.Instance.Services.GetByParam<IStore>("connectionId", Strings.ShopDb);
            IEnumerable<Product> products = store.Load<Product>();
            return View(products);
        }
    }
}