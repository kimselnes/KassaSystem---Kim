using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections;
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
            Separator = "****************************************";
        }
        //public Receipts(List<Product> productList, int amount)
        //{
        //    DateAndTime = DateTime.Now;
        //    ProductList = productList;
        //    Amount = amount;
        //    //TotalPrice = totalPrice;
        //    Separator = "********************";
        //}

        public void PrintToScreen(List<Product> list)
        {
            Console.WriteLine("KVITTO");
            Console.WriteLine(DateAndTime);

            foreach (var product in list)
            {
                Console.WriteLine(
                    $"{product.ProductName} {product.Amount} * " + // ändrade amount till product.Amount
                    $"{product.ProductPrice} = " +
                    $"{product.ProductPrice * product.Amount}"); // ändrade amount till product.Amount

                if (product.ProductName != null)
                {
                    TotalPrice += (product.ProductPrice * product.Amount); // ändrade amount till product.Amount
                }
            }

            Console.WriteLine($"Total: {TotalPrice} kr");

            //if (list == null || amount == 0)
            //{
            //    foreach (var product in list)
            //    {
            //        Console.WriteLine(
            //            $"{product.ProductName} {Amount} * " +
            //            $"{product.ProductPrice} = " +
            //            $"{product.ProductPrice * Amount}");
            //        TotalPrice += (product.ProductPrice * Amount);
            //    }
            //}
            //if (list != null)
            //{
            //    //for (int i = 0; i < list.Count; i++)
            //    //{
            //    //    Console.WriteLine(
            //    //        $"{product.ProductName} {amount} * " +
            //    //        $"{product.ProductPrice} = " +
            //    //        $"{product.ProductPrice * amount}");
            //    //    TotalPrice += (product.ProductPrice * amount);
            //    //}
            //}
        }

        //public List<Product> AddProductToList(Product product, int amount)
        //{
        //    product.Amount = amount;
        //    List<Product> list = new List<Product>();
        //    list.Add(product);

        //    ProductList = list;
        //    return ProductList; 
        //}

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

            Product nullProduct = new Product();
            return nullProduct;
            // MÅSTE FIXA FELHANTERING OM PRODUCT EJ HITTAS
            //throw new Exception("Felaktigt <productId>");
        }

        public void AddReceiptToFile(List<Product> list, int amount)
        {
            var date = DateTime.Today.ToString("yyyyMMdd");
            string filePath = $"../../../Receipt/ReceiptFiles/RECEIPT_{date}.txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            if (File.Exists(filePath))
            {
                var receiptTime = DateTime.Now.ToString();
                var textToAppend = receiptTime + Environment.NewLine;

                foreach (var product in list)
                {
                    textToAppend += $"{product.ProductName} {amount} * " +
                            $"{product.ProductPrice} = " +
                            $"{product.ProductPrice * amount}" + Environment.NewLine;

                    TotalPrice += (product.ProductPrice * amount);
                }
                textToAppend += $"Total: {TotalPrice} kr{Environment.NewLine}{Separator}";

                File.AppendAllText(filePath, textToAppend + Environment.NewLine);
            }
        }


        //public void AddProductToReceiptFile(int productId, int amount)
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
