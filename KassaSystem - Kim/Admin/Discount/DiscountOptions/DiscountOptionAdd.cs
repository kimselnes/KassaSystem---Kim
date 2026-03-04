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
            //int discountId = 0;
            Product discountProd = new Product();

            foreach (var item in sortedProducts)
            {
                for (int i = 0; i < sortedProducts.Count; i++)
                {
                    if (discountIndex == sortedProducts.IndexOf(item, i, 1))
                    {
                        //discountId = item.ProductId;
                        discount.DiscountedProductId = item.ProductId;
                        discountProd = item;
                    }
                }
            }

            Console.Clear();

            //decimal discountSpecificPrice = 0m;
            //double discountPercentPrice = 0;

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
                        Console.WriteLine("Kampanjpriset får inte vara lägre än ordinariepris.");
                        Console.ResetColor();
                        Utility.WaitForKeyPress();

                        DiscountOptionAdd.Run();
                    }
                    else
                    {
                        //discountSpecificPrice = tempDec;
                        discount.DiscountPrice = tempDec;
                    }
                    break;

                case 1:
                    //bool tempBool = true;
                    discount.TwoForThree = true;

                    //var tempBool = SetPercentagePrice();

                    //if (tempDouble == 0d)
                    //{
                    //    DiscountOptionAdd.Run();
                    //}
                    //else
                    //{
                    //    //discountPercentPrice = tempDouble;
                    //    discount.DiscountPercent = tempDouble;

                    //}
                    break;
            }

            Console.WriteLine();
            var startDate = SetStartDate();
            discount.StartDate = startDate;

            Console.WriteLine();
            var endDate = SetEndDate();
            discount.EndDate = endDate;

            DiscountService.WriteToFile(discount);


            Utility.WaitForKeyPress();
            DiscountOption.Menu();
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
                // MAN SKA INTE KUNNA LÄGGA TILL ETT KAMPANJPRIS SOM ÄR HÖGRA ÄN ORDINARIE.
                return discPrice;
            }
        }

        //public static double SetPercentagePrice()
        //{
        //    Console.Write("Ange procentuellt-avdrag under kampanjen: ");
        //    var userInput = Console.ReadLine();

        //    if (!Double.TryParse(userInput, out var disPrice))
        //    {
        //        Console.WriteLine("Felaktigt pris");
        //        return 0d;
        //    }
        //    else
        //    {
        //        return disPrice;
        //    }
        //}

        public static DateTime SetStartDate()
        {
            Console.WriteLine("Ange start datum");
            Console.Write("År 'yyyy': ");
            ////var year = Convert.ToInt32(Console.ReadLine());
            //var userInputYear = Console.ReadLine();

            var userInputYear = ErrorHandling.CheckYearRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputYear, out var year) && year == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }

            //if (userInputYear == null)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Felaktigt år angivet");
            //    Console.ResetColor();
            //    Utility.WaitForKeyPress();
            //    DiscountOptionAdd.Run();
            //}
            //var year = Convert.ToInt32(userInputYear);

            Console.Write("Månad 'MM': ");
            //var month = Convert.ToInt32(Console.ReadLine());
            var userInputMonth = ErrorHandling.CheckMonthDayRegex(Console.ReadLine());
            //if (userInputMonth == null)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Felaktig månad angivet");
            //    Console.ResetColor();
            //    Utility.WaitForKeyPress();
            //    DiscountOptionAdd.Run();
            //}
            //var month = Convert.ToInt32(userInputMonth);

            if (!Int32.TryParse(userInputMonth, out var month) && month == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }

            Console.Write("Dag 'dd': ");
            var userInputDay = ErrorHandling.CheckMonthDayRegex(Console.ReadLine());
            //if (userInputDay == null)
            //{
            //    Console.ForegroundColor = ConsoleColor.Red;
            //    Console.WriteLine("Felaktig dag angivet");
            //    Console.ResetColor();
            //    Utility.WaitForKeyPress();
            //    DiscountOptionAdd.Run();
            //}
            //var day = Convert.ToInt32(userInputDay);
            if (!Int32.TryParse(userInputDay, out var day) && day == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }

            DateTime dateTime = new DateTime(year, month, day);

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
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }


            Console.Write("Dag 'dd': ");
            var userInputDay = ErrorHandling.CheckMonthDayRegex(Console.ReadLine());

            if (!Int32.TryParse(userInputDay, out var day) && day == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                Utility.WaitForKeyPress();
                DiscountOptionAdd.Run();
            }


            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }
    }
}
