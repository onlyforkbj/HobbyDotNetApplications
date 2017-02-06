using ProductManagement.Core.Helpers;
using ProductManagement.Core.Products;
using ProductManagementPortal.Models;
using System.Collections.Generic;
using System.Web.Mvc;


namespace ProductManagementPortal.Controllers
{
    public class HomeController : Controller
    {
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
        public ActionResult Index()
        {
            var productsInStock = SessionProducts;
            if (SessionProducts == null)
            {
                SessionProducts = new ProductFacade<ProductModel>().GetImportedProducts();
            }
            //Cloning the Session Object 
            productsInStock = SessionProducts.GetClone();
            //Removing Products that are OutOfStock
            productsInStock.RemoveAll(p => p.StockQuantity == 0);
            return View(productsInStock);
        }
    }
}