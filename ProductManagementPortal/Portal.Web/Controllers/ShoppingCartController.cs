using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProductManagementPortal.Models;
using ProductManagementPortal.ViewModels;

namespace ProductManagementPortal.Controllers
{
    public class ShoppingCartController : Controller
    {

        private List<Cart> SessionCarts
        {
            get
            {
                return Session["ShoppingCart"] as List<Cart>;
            }
            set
            {
                Session["ShoppingCart"] = value;
            }
        }

        private List<ProductModel> SessionProducts
        {
            get
            {
                return Session["ImportedProducts"] as List<ProductModel>;
            }
            set
            {
                Session["ImportedProducts"] = value;
            }
        }

        string ShoppingCartId { get; set; }

        // GET: ShoppingCart
        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(HttpContext);
            var cartItems = cart.GetCartItems(HttpContext);
            var cartTotal = cart.GetTotal(HttpContext);
            // Set up our ViewModel
            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cartItems,
                CartTotal = cartTotal
            };

            // Return the view
            if (viewModel == null)
            {
                return View(new ShoppingCartViewModel());
            }
            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            // Retrieve the Product from Session
            var addedProduct = SessionProducts.SingleOrDefault(p => p.Id == id);

            // Add it to the shopping cart
            var cart = ShoppingCart.GetCart(HttpContext);

            SessionCarts = cart.AddToCart(addedProduct, HttpContext);

            // Go back to the main store page for more shopping
            return RedirectToAction("Index");
        }

        public JsonResult UpdateCart(int currentQuantity, int productId)
        {
            SessionCarts.Where(w => w.Product.Id == productId).ToList().ForEach(f =>
            {
                f.Count = currentQuantity;
            });
            return Json(true);
        }
    }
}