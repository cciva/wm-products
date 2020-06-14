using Products.Resources;
using Products.Startup;
using Shop.Library;
using Shop.Library.Model;
using Shop.Library.Repository;
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
            IEnumerable<Product> products = ProductsRepo.Load<Product>();
            return View(products);
        }

        public ActionResult New()
        {   
            return View(new Product());
        }

        [HttpPost]
        public ActionResult New(Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Status status = ProductsRepo.Insert<Product>(p);

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

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(int id)
        {
            try
            {
                Product product = this.FindProduct(id);
                //if(TryUpdateModel(product, 
                //    new string[] 
                //    { 
                //        "Description", 
                //        "CategoryId", 
                //        "Make",
                //        "Supplier",
                //        "Price"
                //    }))
                //{
                if(TryUpdateModel(product))
                { 
                    Status status = ProductsRepo.Update<Product>(product);

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

        public ActionResult Delete(int? id)
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

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DoDelete(int id)
        {
            try
            {
                Product product = this.FindProduct(id);

                if (product != null)
                {
                    Status status = ProductsRepo.Delete(product);
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

        [HttpPost, ActionName("TestDelete")]
        public JsonResult TestDelete(int id)
        {
            try
            {
                Product product = this.FindProduct(id);

                if (product != null)
                {
                    Status status = ProductsRepo.Delete(product);
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
            return ProductsRepo.Load<Product>().FirstOrDefault(p => p.Id == id);
        }

        private IRepository ProductsRepo
        {
            get { return _repo; }
        }
    }
}