using KassaSystem___Kim.Products;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Tools
{
    internal class ReadProduct
    {
        static ReadProduct()
        {
        }
        //public ReadProduct(Product product)
        //{

        //}

        public List<Product> SortIntoList()
        {
            string path = "../../../Products/ProductText/Products.csv";
            string filePath = path;

            List<Product> productList = new List<Product>();
            using (StreamReader streamReader = new StreamReader(
                    File.OpenRead(filePath)))
            {
                while (!streamReader.EndOfStream) // EndOfStream will indicate when stream is the end of file.
                {
                    var line = streamReader.ReadLine();
                    var properties = line.Split(';');

                    if (properties[0] != "ProductId")
                    {
                        var product = new Product();
                        product.ProductId = Int32.Parse(properties[0]);
                        product.ProductName = properties[1];
                        product.ProductPrice = Decimal.Parse(properties[2]);
                        product.PriceType = properties[3];
                        productList.Add(product);
                    }
                }
            }

            return productList;
        }
    }
}
