using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Products
{
    internal static class AddProduct
    {
        public static void AddProductToFile(Product product)
        {
            string filePath = "../../../Products/ProductText/Products.csv";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                File.WriteAllText(filePath, 
                    "ProductId;ProductName;ProductPrice,PriceType"
                    + Environment.NewLine);
            }
            if (File.Exists(filePath))
            {
                var textToAppend = $"{product.ProductId};{product.ProductName};" +
                    $"{product.ProductPrice};{product.PriceType}";
                File.AppendAllText(filePath, textToAppend + Environment.NewLine);
            }
        }
    }
}
