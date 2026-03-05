using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.Discount.DiscountOptions
{
    internal static class DiscountOptionAdd
    {
        internal static void Run()
        {
            Console.Clear();

            var readProduct = new ReadProduct();
            var sortedProducts = readProduct.SortIntoList();

            List<string> tempListToArray = new List<string>();
            foreach (var product in sortedProducts)
            {
                string tempString = $"[ {product.ProductId} " +
                    $"{product.ProductName} " +
                    $"{product.ProductPrice} " +
                    $"{product.PriceType} ]";

                tempListToArray.Add(tempString);
            }
            string[] discountOptions = tempListToArray.ToArray();

            string discountPrompt = "Ange den varan du vill sätta kampanjpris på";
            Menu discountMenu = new Menu(discountPrompt, discountOptions);
            int discountIndex = discountMenu.Run();

            Discount discount = new Discount();
            Product discountProd = new Product();

            foreach (var item in sortedProducts)
            {
                for (int i = 0; i < sortedProducts.Count; i++)
                {
                    if (discountIndex == sortedProducts.IndexOf(item, i, 1))
                    {
                        discount.DiscountedProductId = item.ProductId;
                        discountProd = item;
                    }
                }
            }

            Console.Clear();

            string prompt = $"Ordinarie pris: {discountProd.ProductPrice}";
            string[] options = ["Specifikt pris", "Ta tre, betala for 2"];
            Menu menu = new Menu(prompt, options);
            int selectedIndex = menu.Run();

            switch (selectedIndex)
            {
                case 0:
                    var tempDec = SetDiscountPrice();

                    if (tempDec == 0m)
                    {
                        DiscountOptionAdd.Run();
                    }
                    else if (tempDec >= discountProd.ProductPrice)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Kampanjpriset får inte vara högre än ordinariepris.");
                        Console.ResetColor();
                        Utility.WaitForKeyPress();

                        DiscountOptionAdd.Run();
                    }
                    else
                    {
                        discount.DiscountPrice = tempDec;
                    }
                    break;

                case 1:
                    discount.TwoForThree = true;
                    break;
            }

            Console.WriteLine();
            var startDate = SetStartDate();
            discount.StartDate = startDate;

            Console.WriteLine();
            var endDate = SetEndDate();
            discount.EndDate = endDate;

            if (endDate > startDate)
            {
                DiscountService.WriteToFile(discount);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nDu har lagt till en kampanj");
                Console.ResetColor();

                Utility.WaitForKeyPress();
                DiscountOption.Menu();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nDu måste ange ett slutdatum som ligger efter startdatumet");
                Console.ResetColor();

                Utility.WaitForKeyPress();
                DiscountOption.Menu();
            }

        }

        public static decimal SetDiscountPrice()
        {
            Console.Write("Ange nytt pris under kampanjen: ");
            var userInput = Console.ReadLine();

            if (!Decimal.TryParse(userInput, out var discPrice))
            {
                Console.WriteLine("Felaktigt pris");
                return 0m;
            }
            else
            {
                return discPrice;
            }
        }

        public static DateTime SetStartDate()
        {
            Console.WriteLine("Ange start datum");
            Console.Write("År 'yyyy': ");

            var userInputYear = ErrorHandling.CheckYearRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputYear, out var year))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }

            Console.Write("Månad 'MM': ");
            var userInputMonth = ErrorHandling.CheckMonthDayRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputMonth, out var month))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktig månad angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }


            Console.Write("Dag 'dd': ");
            var userInputDay = ErrorHandling.CheckMonthDayRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputDay, out var day))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktig dag angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }


            DateTime dateTime;
            try
            {
                dateTime = new DateTime(year, month, day);
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nFelaktigt datum angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
                throw;
            }


            var currentDate = DateTime.Now.Date;
            if (dateTime.Date < currentDate)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Du måste ange dagens datum eller senare.");
                Console.ResetColor();

                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }

            return dateTime;
        }

        public static DateTime SetEndDate()
        {
            Console.WriteLine("Ange slut datum");
            Console.Write("År 'yyyy': ");
            var userInputYear = ErrorHandling.CheckYearRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputYear, out var year) && year == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }


            Console.Write("Månad 'MM': ");
            var userInputMonth = ErrorHandling.CheckMonthDayRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputMonth, out var month) && month == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktig månad angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }


            Console.Write("Dag 'dd': ");
            var userInputDay = ErrorHandling.CheckMonthDayRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputDay, out var day) && day == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktig dag angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }


            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }
    }
}
