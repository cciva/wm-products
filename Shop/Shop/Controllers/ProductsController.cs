using Shop;
using Shop.Library;
using Shop.Library.Model;
using Shop.Library.Repository;
using Shop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Management;
using System.Web.Mvc;

namespace Products.Controllers
{
    public class ProductsController : Controller
    {
        IRepository _repo = null;

        public ProductsController()
        {
            _repo = ShopApp.Instance.Services.Get<IRepository>();
        }

        public ActionResult Browse()
        {
            IEnumerable<Product> products = Repository.Load<Product>();
            return View(products);
        }

        public ActionResult New()
        {
            IEnumerable<Category> cat = Repository.Load<Category>();
            return View(new ProductViewModel(cat));
        }

        [HttpPost]
        public ActionResult New(ProductViewModel pv)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Status status = Repository.Insert(pv.Product);

                    if (status.Success)
                    {
                        ViewBag.Message = "Employee details added successfully";
                        return RedirectToAction("Browse", "Products");
                    }
                }

                return View();
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Product product = this.FindProduct(id.Value);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(new ProductViewModel(product, Repository.Load<Category>()));
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            try
            {
                Product product = this.FindProduct(id);
                ProductViewModel vm = new ProductViewModel(product, Repository.Load<Category>());

                if (TryUpdateModel(vm))
                { 
                    Status status = Repository.Update(vm.Product);

                    if (status.Success)
                    {
                        return RedirectToAction("Browse", "Products");
                    }
                }                

                return View();
            }
            catch (Exception err)
            {
                return View();
            }            
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            try
            {
                Product product = this.FindProduct(id);

                if (product != null)
                {
                    Status status = Repository.Delete(product);
                    return Json(status);
                }

                return Json(new Status(new Exception("Product not found")));
            }
            catch (Exception err)
            {
                return Json(new Status(err));
            }
        }

        private Product FindProduct(int id)
        {
            return Repository.Load<Product>().FirstOrDefault(p => p.Id == id);
        }

        private IRepository Repository
        {
            get { return _repo; }
        }
    }
}