using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Products
{
    internal class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string PriceType { get; set; }
        public int Amount { get; set; }
        public bool HasDiscount { get; set; } = false;

        public Product()
        {
            Amount = 0;
        }
        public Product(int productId, string productName, decimal productPrice, string priceType)
        {
            ProductId = productId;
            ProductName = productName;
            ProductPrice = productPrice;
            PriceType = priceType; // Kanske inte ha ToUpper() här!
        }
    }
}
