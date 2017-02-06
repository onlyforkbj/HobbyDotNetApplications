using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductManagementPortal.Models
{
    public class ShoppingCart
    {
        public const string CartSessionKey = "CartId";
        string ShoppingCartId { get; set; }

        private List<Cart> SessionCarts
        {
            get
            {
                return HttpContext.Current.Session["ShoppingCart"] as List<Cart>;
            }
            set
            {
                HttpContext.Current.Session["ShoppingCart"] = value;
            }
        }

        public List<Cart> AddToCart(ProductModel product, HttpContextBase context)
        {
            Cart cartItem = null;
            List<Cart> shoppingCartItems = new List<Cart>();
            if (SessionCarts != null)
            {
                shoppingCartItems = SessionCarts;
                cartItem =
                    shoppingCartItems.SingleOrDefault(c => c.CartId == ShoppingCartId && c.Product.Id == product.Id);

            }
            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                var cart = new Cart
                {
                    ProductId = product.Id,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now,
                    Product = product
                };
                shoppingCartItems.Add(cart);
            }
            else
            {
                // If the item does exist in the cart, then add one to the quantity
                cartItem.Count++;
            }

            // Save changes
            if (context.Session != null) SessionCarts = shoppingCartItems;
            return shoppingCartItems;
        }

        public static ShoppingCart GetCart(HttpContextBase context)
        {
            var cart = new ShoppingCart();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }

        // Helper method to simplify shopping cart calls
        public static ShoppingCart GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }

        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContextBase context)
        {
            if (context.Session == null || context.Session[CartSessionKey] != null)
                return context.Session?[CartSessionKey].ToString();
            if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
            {
                context.Session[CartSessionKey] = context.User.Identity.Name;
            }
            else
            {
                // Generate a new random GUID using System.Guid class
                Guid tempCartId = Guid.NewGuid();

                // Send tempCartId back to client as a cookie
                context.Session[CartSessionKey] = tempCartId.ToString();
            }
            return context.Session?[CartSessionKey].ToString();
        }

        public List<Cart> GetCartItems(HttpContextBase context)
        {
            return SessionCarts?.Where(cart => cart.CartId == ShoppingCartId).ToList();
        }

        public decimal GetTotal(HttpContextBase context)
        {
            var total =
                SessionCarts?.Where(ci => ci.CartId == ShoppingCartId)
                    .Select(cartItems => (int?)cartItems.Count * cartItems.Product.Price)
                    .Sum();

            return total ?? decimal.Zero;
        }
    }
}