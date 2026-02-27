using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public void AddTotalPrice(Product product)
        {
            TotalPrice += product.ProductPrice * product.Amount;
        }

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

                //if ()
                //{
                //    TotalPrice += (product.ProductPrice * product.Amount); // ändrade amount till product.Amount
                //}
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
            var readSortProducts = readProduct.SortIntoList();

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

        public void AddReceiptToFile(List<Product> list, int serialNumber)
        {
            var date = DateTime.Today.ToString("yyyyMMdd");
            string filePath = $"../../../Receipt/ReceiptFiles/RECEIPT_{date}.txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
            if (File.Exists(filePath))
            {
                serialNumber++;
                SetSerialNumberText(serialNumber);

                var receiptTime = DateTime.Now.ToString();
                var textToAppend = "Serial number: " + 
                    serialNumber + Environment.NewLine + 
                    receiptTime + Environment.NewLine;

                foreach (var product in list)
                {
                    textToAppend += $"{product.ProductName} {product.Amount} * " +
                            $"{product.ProductPrice} = " +
                            $"{product.ProductPrice * product.Amount}" + Environment.NewLine;

                    TotalPrice += (product.ProductPrice * product.Amount);
                }
                textToAppend += $"Total: {TotalPrice} kr{Environment.NewLine}{Separator}";

                File.AppendAllText(filePath, textToAppend + Environment.NewLine);
            }
        }

        public int GetSerialNumber()
        {
            string filePath = $"../../../Receipt/SerialNumber/SerialNumber.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            int serialNumber = 0;

            using (StreamReader streamReader = new StreamReader(
                    File.OpenRead(filePath)))
            {
                while (!streamReader.EndOfStream) // EndOfStream will indicate when stream is the end of file.
                {
                    var line = streamReader.ReadLine();
                    serialNumber = Convert.ToInt32(line);
                }
            }
            return serialNumber;
        }

        public void SetSerialNumberText(int serialNumber)
        {
            string filePath = $"../../../Receipt/SerialNumber/SerialNumber.txt";

            using (StreamWriter streamWriter = new StreamWriter(filePath))
            {
                streamWriter.WriteLine(serialNumber.ToString());
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
