using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Products
{
    internal static class AddProduct
    {
        //public int ProductId { get; set; }
        //public string ProductName { get; set; }
        //public decimal ProductPrice { get; set; }
        //public string PriceType { get; set; }

        //public AddProduct(int productId, string productName,
        //    decimal productPrice, string priceType)
        //{
        //    ProductId = productId;
        //    ProductName = productName;
        //    ProductPrice = productPrice;
        //    PriceType = priceType.ToUpper();
        //}
        //public CreateProduct(Product product)
        //{
        //    product.ProductId = productId;
        //    ProductName = productName;
        //    ProductPrice = productPrice;
        //    PriceType = priceType.ToUpper();
        //}

        //public void AddProductToFile(CreateProduct product)
        //{
        //    string filePath = "../../../Products/ProductText/Products.csv";

        //    if (!File.Exists(filePath))
        //    {
        //        File.Create(filePath).Close();
        //    }
        //    if(File.Exists(filePath))
        //    {
        //        using (StreamWriter sWriter = new StreamWriter(filePath)) 
        //        {
        //            sWriter.WriteLine($"{product.ProductId};{product.ProductName};" +
        //                $"{product.ProductPrice};{product.PriceType}");
        //        }
        //    }


        //}

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




                //using (StreamWriter sWriter = new StreamWriter(filePath))
                //{
                //    sWriter.WriteLine($"{product.ProductId};{product.ProductName};" +
                //    $"{product.ProductPrice};{product.PriceType}");
                //}

                //File.AppendAllText(filePath, $"{product.ProductId};{product.ProductName};" +
                //    $"{product.ProductPrice};{product.PriceType}");
            }
        }
    }
}
