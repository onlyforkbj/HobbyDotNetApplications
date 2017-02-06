using System;

namespace ProductManagementPortal.Models
{
    [Serializable]
    public class ProductModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int StockQuantity { get; set; }
    }
}