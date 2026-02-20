using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Receipt
{
    internal class Receipts
    {
        public DateTime DateAndTime { get; set; }
        public List<Product> ProductList { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
        public string Separator { get; set; }

        public Receipts()
        {
            DateAndTime = DateTime.Now;
        }
        public Receipts(List<Product> productList, int amount)
        {
            DateAndTime = DateTime.Now;
            ProductList = productList;
            Amount = amount;
            //TotalPrice = totalPrice;
            Separator = "********************";
        }

        public void PrintToScreen()
        {
            Console.WriteLine("KVITTO");
            Console.WriteLine(DateAndTime);

            if (ProductList != null)
            {
                foreach (var product in ProductList)
                {
                    Console.WriteLine(
                        $"{product.ProductName} {Amount} * " +
                        $"{product.ProductPrice} = " +
                        $"{product.ProductPrice * Amount}");
                    TotalPrice += (product.ProductPrice * Amount);
                }
            }

            Console.WriteLine($"Total: {TotalPrice} kr");
        }

        public static Product FindProduct(int productId)
        {
            var readProduct = new ReadProduct();
            var readSortProducts = readProduct.ReadSortProductList();

            foreach (var product in readSortProducts)
            {
                if (productId == product.ProductId)
                {
                    return product;
                }                
            }

            throw new Exception("Felaktigt <productId>");
        }

        //public void AddProductToReceipt(int productId, int amount)
        //{
        //    var products = ReadProduct.ReadSortProductList();

        //    foreach (var product in product)
        //    {
        //        product.ProductName = properties[1];
        //        product.ProductPrice = Decimal.Parse(properties[2]);
        //    }
        //}
    }
}
