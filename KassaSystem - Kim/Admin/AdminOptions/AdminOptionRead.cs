using KassaSystem___Kim.Admin.Discount;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.AdminOptions
{
    internal static class AdminOptionRead
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("Alla produkter\n");

            var readProducts = new ReadProduct();
            var products = readProducts.SortIntoList();

            foreach (var product in products)
            {
                Console.WriteLine(
                    $"[ {product.ProductId} " +
                    $"{product.ProductName} " +
                    $"{product.ProductPrice} " +
                    $"{product.PriceType} ]");
            }

            Console.WriteLine();
            Utility.WaitForKeyPress();
            DiscountOption.Menu();
        }
    }
}
