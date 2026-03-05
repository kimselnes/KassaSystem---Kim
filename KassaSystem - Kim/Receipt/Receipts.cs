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

        public void AddTotalPrice(Product product)
        {
            if (product.HasTwoThreeDiscount)
            {
                var threeForTwo = product.Amount / 3;
                TotalPrice += product.ProductPrice * (product.Amount - threeForTwo);
            }
            else
            {
                TotalPrice += product.ProductPrice * product.Amount;
            }
        }

        public void PrintToScreen(List<Product> list, List<Product> originalList) // NYVERSION som tar med KAMPANJER
        {
            var originalPrice = 0m;
            Console.WriteLine("KVITTO");
            Console.WriteLine(DateAndTime);

            foreach (var product in list)
            {
                foreach (var originalProduct in originalList)
                {
                    if (product.ProductId == originalProduct.ProductId)
                    {
                        originalPrice = originalProduct.ProductPrice;
                    }
                }

                if (product.HasDiscount)
                {
                    if (product.HasTwoThreeDiscount &&
                        product.ProductPrice != originalPrice)
                    {
                        // Fixa så att var tredje vara blir gratis.
                        int threeForTwo = product.Amount / 3;
                        int threeForTwoResult = product.Amount - threeForTwo;

                        Console.WriteLine(
                        $"{product.ProductName} {threeForTwoResult} * " +
                        $"{product.ProductPrice} = " +
                        $"{product.ProductPrice * threeForTwoResult}\n" +
                        $"  -Ordinarie: {product.Amount} * {originalPrice} = {originalPrice * product.Amount}");
                    }
                    else if (product.HasTwoThreeDiscount)
                    {
                        // Rabatt pris
                        Console.WriteLine(
                        $"{product.ProductName} {product.Amount} * " +
                        $"{product.ProductPrice} = " +
                        $"{product.ProductPrice * product.Amount}");
                    }
                    else if (product.ProductPrice != originalPrice)
                    {
                        Console.WriteLine(
                        $"{product.ProductName} {product.Amount} * " +
                        $"{product.ProductPrice} = " +
                        $"{product.ProductPrice * product.Amount}\n" +
                        $"  -Ordinariepris: {originalPrice * product.Amount}");
                    }
                }
                else
                {
                    Console.WriteLine(
                        $"{product.ProductName} {product.Amount} * " +
                        $"{product.ProductPrice} = " +
                        $"{product.ProductPrice * product.Amount}");
                }
            }

            Console.WriteLine($"Total: {TotalPrice} kr");
        }

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
        }

        public void AddReceiptToFile( List<Product> list, 
            int serialNumber, List<Product> originalList)
        {
            var originalPrice = 0m;

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
                    foreach (var originalProduct in originalList)
                    {
                        if (product.ProductId == originalProduct.ProductId)
                        {
                            originalPrice = originalProduct.ProductPrice;
                        }
                    }

                    if (product.HasDiscount)
                    {
                        if (product.HasTwoThreeDiscount)
                        {
                            int threeForTwo = product.Amount / 3;

                            textToAppend += $"{product.ProductName} {product.Amount - threeForTwo} * " +
                                    $"{product.ProductPrice} = " +
                                    $"{product.ProductPrice * (product.Amount - threeForTwo)}\n" +
                                    $"  -Ordinarie: {product.Amount} * {originalPrice} = {originalPrice * product.Amount}"
                                    + Environment.NewLine;

                            TotalPrice += (product.ProductPrice * (product.Amount - threeForTwo));
                        }
                        else
                        {
                            textToAppend += $"{product.ProductName} {product.Amount} * " +
                                    $"{product.ProductPrice} = " +
                                    $"{product.ProductPrice * product.Amount}\n" +
                                    $"  -Ordinariepris: {originalPrice * product.Amount}"
                                    + Environment.NewLine;

                            TotalPrice += (product.ProductPrice * product.Amount);
                        }
                    }
                    else
                    {
                        textToAppend += $"{product.ProductName} {product.Amount} * " +
                                $"{product.ProductPrice} = " +
                                $"{product.ProductPrice * product.Amount}" + Environment.NewLine;

                        TotalPrice += (product.ProductPrice * product.Amount);
                    }

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
    }
}
