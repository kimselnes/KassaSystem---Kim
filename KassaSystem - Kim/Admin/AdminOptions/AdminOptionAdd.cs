using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.AdminOptions
{
    internal static class AdminOptionAdd
    {
        public static void Run()
        {
            Console.Clear();

            Console.WriteLine("Lägga till produkt\n");
            Console.Write("Produkt ID: "); // Lägga till så att produktId bara kan bara 3 siffror.
                                           //int pId = Int32.Parse(Console.ReadLine());
                                           //string regexPattern = @"\b[0-9]{3}\b"; // Vill att det ska vara endast 3 siffror.
                                           //Regex regex = new(regexPattern);

            ////if (!Int32.TryParse(Console.ReadLine(), out var pId))
            ////{
            ////    Console.WriteLine("Ange 3 siffrigt Produkt ID.");
            ////    Utility.WaitForKeyPress();
            ////    AdminOption.Menu();
            ////}
            //string input = Console.ReadLine();
            //Match match = regex.Match(input);

            //if (!match.Success)
            //{
            //    Console.WriteLine("Ange 3 siffrigt Produkt ID.");
            //    Utility.WaitForKeyPress();
            //    AdminOption.Menu();
            //}
            //int pId = Convert.ToInt32(input);

            string userInput = Console.ReadLine();
            bool isIdRegexOk = ErrorHandling.CheckIdRegex(userInput);
            if (!isIdRegexOk)
            {
                AdminOption.Menu();
            }
            int pId = Convert.ToInt32(userInput);


            Console.Write("Produkt namn: ");
            string pName = Console.ReadLine();


            Console.Write("Pris: ");
            //Decimal.TryParse(Console.ReadLine(), out var pPrice); // Se till att det inte blir fel om man använder '.' istället för ','.
            if (!Decimal.TryParse(Console.ReadLine(), out var pPrice))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ange korrekt pris. Använd ',' inte '.'");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                AdminOption.Menu();
            }


            Console.Write("Kilo eller Styck pris? KG/ST: ");
            string pType = Console.ReadLine().ToUpper();

            if (!ErrorHandling.CheckPriceType(pType))
            {
                Utility.WaitForKeyPress();
                AdminOption.Menu();
            }

            var newProduct = new Product(pId, pName, pPrice, pType);
            var readProduct = new ReadProduct();
            List<Product> products = new List<Product>();
            products = readProduct.SortIntoList();

            if (ErrorHandling.CheckDuplicate(products, newProduct))
            {
                Utility.WaitForKeyPress();
                AdminOption.Menu();
            }
            else
            {
                AddProduct.AddProductToFile(newProduct);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nDu har lagt till:" +
                    $"'{pName}'" +
                    $" med ID '{pId}'" +
                    $" och priset '{pPrice}{pType}'.\n");
                Console.ResetColor();
                Utility.WaitForKeyPress();
            }

            AdminOption.Menu();
        }
    }
}
