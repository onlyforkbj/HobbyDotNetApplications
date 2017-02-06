using AutoMapper;
using ProductManagement.Core.Products;
using ProductManagementPortal.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementPortal.Controllers
{
    public class ProductController : Controller
    {

        private readonly IProductFacade<ProductModel> _productFacade;
        private const string ImportedProducts = "ImportedProducts";

        private List<ProductModel> SessionProducts
        {
            get
            {
                return Session[ImportedProducts] as List<ProductModel>;
            }
            set
            {
                Session[ImportedProducts] = value;
            }
        }

        public ProductController()
        {
        }

        public ProductController(IProductFacade<ProductModel> productFacade)
        {
            _productFacade = productFacade;
        }

        public ActionResult ProductAdministration()
        {
            if (SessionProducts == null)
            {
                SessionProducts = new ProductFacade<ProductModel>().GetImportedProducts();
            }
            return View(SessionProducts);
        }

        public ActionResult Delete(int id)
        {
            SessionProducts.RemoveAll(p => p.Id == id);
            return RedirectToAction("ProductAdministration");
        }

        public ActionResult Details(int id)
        {
            return View(SessionProducts.FirstOrDefault(prod => prod.Id == id));
        }

        public ActionResult Edit(int id)
        {
            return View(SessionProducts.FirstOrDefault(product => product.Id == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductModel product)
        {
            if (!ModelState.IsValid) return RedirectToAction("ProductAdministration");
            var updatedProduct = Mapper.Map(product, SessionProducts.SingleOrDefault(p => p.Id == product.Id));
            SessionProducts.RemoveAll(p => p.Id == product.Id);
            SessionProducts.Add(updatedProduct);
            return RedirectToAction("ProductAdministration");
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    //var path = Path.Combine(Server.MapPath("~/App_Data/Images"), fileName);
                    var products = new ProductFacade<ProductModel>().UploadProuctCatalogue(file.FileName);
                    SessionProducts = products;
                }
            }
            return RedirectToAction("ProductAdministration");
        }

        public ActionResult Save(int id)
        {
            return RedirectToAction("ProductAdministration");
        }
    }
}