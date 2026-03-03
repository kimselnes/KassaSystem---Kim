using KassaSystem___Kim.Admin;
using KassaSystem___Kim.Admin.Discount;
using KassaSystem___Kim.Products;
using KassaSystem___Kim.Receipt;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim
{
    internal class App
    {

        public void Start()
        {
            // if catches error do Run() again?
            // "error message"
            // WaitForKeyPress()
            // Run()

            Console.Title = "Kim's Kassasystem";

            RunMainMenu();
        }

        public void RunMainMenu()
        {
            string prompt = "Kassa System";
            string[] options = ["Ny kund", "Admin meny", "Avsluta", "TEST"]; // TA BORT TEST.

            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Register();
                    //Utility.WaitForKeyPress();
                    //RunMainMenu();
                    break;

                case 1:
                    AdminOption.Menu();
                    break;

                case 2:
                    Utility.ExitApp();
                    break;
                case 3:
                    // Här TESTAR jag klasser/metoder under utvecklingen. TA BORT!!
                    List<Discount> discountList = DiscountService.SortIntoList();
                    //DiscountOption.Menu();
                    Utility.WaitForKeyPress();
                    break;
            }
        }

        //private void Register()
        //{
        //    // Kassan
        //    // Här sker betalningen.
        //    // Om man matar in <productId> och <amount> som "300 4" - (MÅSTE SKRIVAS PÅ DETTA VIS OCH HA MELLANSLAG)
        //    // behöver man separera numre(r)na  och identifiera de som productId och amount.
        //    // Typ lägga i en array och se [0] som productId och [1] som amount.
        //    var receipt = new Receipts();
        //    var registerProducts = new List<Product>();
        //    var foundProduct = new Product();
        //    int[] separatedCommand;
        //    bool notPayed = true;

        //    while (notPayed)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Kassa\n");

        //        receipt.PrintToScreen(registerProducts, foundProduct.Amount);

        //        // UserINPUT
        //        Console.WriteLine("\nKommandon:");
        //        Console.WriteLine("<produktId> <Antal>");
        //        Console.WriteLine("PAY\n");
        //        Console.Write("Kommando:");
        //        var userInput = Console.ReadLine();

        //        if (userInput == "PAY")
        //        {
        //            Receipts receipts = new Receipts();
        //            receipts.AddReceiptToFile(registerProducts, foundProduct.Amount);
        //            notPayed = false;
        //        }
        //        else if (true)
        //        {
        //            // Separate ID and AMOUNT
        //            separatedCommand = Utility.SeparateCommand(userInput);

        //            foundProduct = Receipts.FindProduct(separatedCommand[0]);
        //            if (foundProduct.ProductName != null)
        //            {
        //                foundProduct.Amount = separatedCommand[1];
        //                registerProducts.Add(foundProduct);
        //            }
        //            else
        //            {

        //            }

        //        }
        //        //receipt.PrintToScreen(foundProduct, separatedCommand[1]);
        //    }



        //Console.WriteLine();
        //Utility.WaitForKeyPress();
        //RunMainMenu();
        //}
        private void Register() // KOPIA, SKA SEI FALL JAG KAN LÖSA AMOUNT ISSUE.
        {
            // Kassan
            // Här sker betalningen.
            // Om man matar in <productId> och <amount> som "300 4" - (MÅSTE SKRIVAS PÅ DETTA VIS OCH HA MELLANSLAG)
            // behöver man separera numre(r)na  och identifiera de som productId och amount.
            // Typ lägga i en array och se [0] som productId och [1] som amount.
            var receipt = new Receipts();
            int serialNumber = receipt.GetSerialNumber();
            var registerProducts = new List<Product>();
            var foundProduct = new Product();
            List<Discount> discountList = DiscountService.SortIntoList();
            decimal originalProductPrice = 0m;
            int[] separatedCommand;
            bool notPayed = true;

            while (notPayed)
            {
                Console.Clear();
                Console.WriteLine("Kassa\n");

                receipt.PrintToScreen2(registerProducts, originalProductPrice);

                // UserINPUT
                Console.WriteLine("\nKommandon:");
                Console.WriteLine("<produktId> <Antal>");
                Console.WriteLine("PAY\n");
                Console.Write("Kommando:");
                var userInput = Console.ReadLine();

                if (userInput.ToUpper() == "PAY")
                {
                    Receipts receipts = new Receipts();
                    receipts.AddReceiptToFile(
                        registerProducts, 
                        serialNumber, 
                        originalProductPrice);
                    notPayed = false;
                }
                else if (true)
                {
                    // Separate ID and AMOUNT
                    separatedCommand = Utility.SeparateCommand(userInput);

                    foundProduct = Receipts.FindProduct(separatedCommand[0]);

                    // ÄR DET HÄR JAG VILL KOLLA EFTER EVENTUELL KAMPANJ?
                    foreach (var discount in discountList)
                    {
                        if (foundProduct.ProductId == discount.DiscountedProductId)
                        {
                            if (discount.DiscountPrice != 0)
                            {
                                originalProductPrice = foundProduct.ProductPrice;
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





                    if (foundProduct.ProductName != null)
                    {
                        foundProduct.Amount = separatedCommand[1];
                        registerProducts.Add(foundProduct);
                        receipt.AddTotalPrice(foundProduct);
                    }
                }
            }

            Console.WriteLine();
            Utility.WaitForKeyPress();
            RunMainMenu();
        }
    }
}
