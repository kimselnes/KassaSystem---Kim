using KassaSystem___Kim.Admin;
using KassaSystem___Kim.Admin.Discount;
using KassaSystem___Kim.Products;
using KassaSystem___Kim.Receipt;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace KassaSystem___Kim
{
    internal class App
    {

        public void Start()
        {
            Console.Title = "Kim's Kassasystem";

            RunMainMenu();
        }

        public void RunMainMenu()
        {
            string prompt = "Kassa System";
            string[] options = ["Ny kund", "Admin meny", "Avsluta"];

            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Register();
                    break;

                case 1:
                    AdminOption.Menu();
                    break;

                case 2:
                    Utility.ExitApp();
                    break;
            }
        }

        private void Register()
        {
            var receipt = new Receipts();
            int serialNumber = receipt.GetSerialNumber();
            var registerProducts = new List<Product>();
            var readProducts = new ReadProduct();
            var originalListOfProducts = readProducts.SortIntoList();
            var foundProduct = new Product();
            List<Discount> discountList = DiscountService.SortIntoList();
            int[] separatedCommand;
            bool showListOfProducts = true;
            bool notPayed = true;

            while (notPayed)
            {
                Console.Clear();
                Console.WriteLine("Kassa\n");

                if (showListOfProducts == true)
                {
                    foreach (var product in originalListOfProducts)
                    {
                        Console.WriteLine(
                            $"[ {product.ProductId} " +
                            $"{product.ProductName} " +
                            $"{product.ProductPrice} " +
                            $"{product.PriceType} ]");
                    }
                    Console.WriteLine();
                }

                receipt.PrintToScreen(registerProducts, originalListOfProducts);

                // UserINPUT
                Console.WriteLine("\nKommandon:");
                Console.WriteLine("<produktId> <antal>");
                Console.WriteLine("LIST (Visa/ta bort lista av produkter)");
                Console.WriteLine("PAY\n");
                Console.Write("Kommando:");
                var userInput = Console.ReadLine();

                if (userInput.ToUpper() == "PAY")
                {
                    Receipts receipts = new Receipts();
                    receipts.AddReceiptToFile(
                        registerProducts,
                        serialNumber,
                        originalListOfProducts);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nDu har betalat för dina varor");
                    Console.ResetColor();
                    notPayed = false;
                }
                else if (userInput.ToUpper() == "LIST")
                {
                    if (showListOfProducts == true)
                    {
                        showListOfProducts = false;
                    }
                    else
                    {
                        showListOfProducts = true;
                    }
                }
                else if (true)
                {
                    // Separate ID and AMOUNT
                    separatedCommand = Utility.SeparateCommand(userInput);
                    foundProduct = Receipts.FindProduct(separatedCommand[0]);

                    // Checks discounts
                    foreach (var discount in discountList)
                    {
                        if (discount.StartDate <= DateTime.Now.Date &&
                            discount.EndDate >= DateTime.Now.Date)
                        {
                            if (foundProduct.ProductId == discount.DiscountedProductId)
                            {
                                if (discount.DiscountPrice != 0 &&
                                    discount.DiscountPrice < foundProduct.ProductPrice) // ADDED && discount.DiscountPrice < foundProduct.ProductPrice
                                {
                                    foundProduct.ProductPrice = discount.DiscountPrice;
                                    foundProduct.HasDiscount = true;
                                }
                                else if (discount.TwoForThree)
                                {
                                    foundProduct.HasTwoThreeDiscount = true;
                                    foundProduct.HasDiscount = true;
                                }
                            }
                        }
                    }

                    if (foundProduct.ProductName != null)
                    {
                        foundProduct.Amount = separatedCommand[1];
                        registerProducts.Add(foundProduct);
                        receipt.AddTotalPrice(foundProduct);
                    }
                }
            }

            Utility.WaitForKeyPress();
            RunMainMenu();
        }
    }
}
