using KassaSystem___Kim.Admin;
using KassaSystem___Kim.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace KassaSystem___Kim.Tools
{
    internal static class ErrorHandling
    {
        public static bool CheckIdRegex(string userInput)
        {
            string regexPattern = @"\b[0-9]{3}\b";
            Regex regex = new(regexPattern);

            Match match = regex.Match(userInput);

            if (!match.Success)
            {
                Console.WriteLine("Ange 3 siffrigt Produkt ID.");
                Utility.WaitForKeyPress();
                return false;
            }
            else
            {
                return true;
            }
        }

        public static string CheckYearRegex(string userInput)
        {
            string regexPattern = @"\b[0-9]{4}\b";
            Regex regex = new(regexPattern);

            Match match = regex.Match(userInput);

            if (!match.Success)
            {
                return null;
            }
            else
            {
                return userInput;
            }
        }

        public static string CheckMonthDayRegex(string userInput)
        {
            string regexPattern = @"\b[0-9]{2}\b";
            Regex regex = new(regexPattern);

            Match match = regex.Match(userInput);

            if (!match.Success)
            {
                return null;
            }
            else
            {
                return userInput;
            }
        }

        public static bool CheckDuplicate(List<Product> sortedList, Product newProduct)
        {
            foreach (var item in sortedList)
            {
                if (item.ProductId == newProduct.ProductId)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du har använt samma ID från annan produkt.");
                    Console.ResetColor();

                    return true;
                }
            }
            return false;
        }

        public static bool CheckPriceType(string priceType)
        {
            if (priceType.ToUpper() == "KG" || priceType.ToUpper() == "ST")
            {
                return true;
            }
            else
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Du angav felaktig typ (KG eller ST)");
                Console.ResetColor();
                return false;
            }
        }
    }
}
