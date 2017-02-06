using System.Collections.Generic;
using ProductManagementPortal.Models;

namespace ProductManagementPortal.ViewModels
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartViewModel()
        {
            CartItems = new List<Cart>();
        }
        public List<Cart> CartItems { get; set; }
        public decimal CartTotal { get; set; }
    }
}