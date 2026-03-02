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
        }

        public static decimal SetDiscountPrice()
        {
            Console.Write("Ange nytt pris under kampanjen: ");
            var userInput = Console.ReadLine();

            if (!Decimal.TryParse(userInput, out var disPrice))
            {
                Console.WriteLine("Felaktigt pris");
                return 0m;
            }
            else
            {
                // MAN SKA INTE KUNNA LÄGGA TILL ETT KAMPANJPRIS SOM ÄR HÖGRA ÄN ORDINARIE.
                return disPrice;
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
            //var year = Convert.ToInt32(Console.ReadLine());
            if (!Int32.TryParse(Console.ReadLine(), out var year))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt år angivet");
                Console.ResetColor();
                DiscountOptionAdd.Run();
            }
            Console.Write("Månad 'MM': ");
            //var month = Convert.ToInt32(Console.ReadLine());
            if (!Int32.TryParse(Console.ReadLine(), out var month))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktig månad angivet");
                Console.ResetColor();
                DiscountOptionAdd.Run();
            }
            Console.Write("Månad 'dd': ");
            //var day = Convert.ToInt32(Console.ReadLine());
            if (!Int32.TryParse(Console.ReadLine(), out var day))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Felaktigt datum angivet");
                Console.ResetColor();
                DiscountOptionAdd.Run();
            }

            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }

        public static DateTime SetEndDate()
        {
            Console.WriteLine("Ange slut datum");
            Console.Write("År 'yyyy': ");
            var year = Convert.ToInt32(Console.ReadLine());
            Console.Write("Månad 'MM': ");
            var month = Convert.ToInt32(Console.ReadLine());
            Console.Write("Månad 'dd': ");
            var day = Convert.ToInt32(Console.ReadLine());

            DateTime dateTime = new DateTime(year, month, day);

            return dateTime;
        }
    }
}
