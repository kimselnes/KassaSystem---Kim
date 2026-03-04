using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.Discount.DiscountOptions
{
    internal static class DiscountOptionRead
    {
        public static void Run()
        {
            Console.Clear();
            Console.WriteLine("Alla skapta kampanjer\n");

            var discounts = DiscountService.SortIntoList();

            foreach (var discount in discounts)
            {
                Console.WriteLine(
                    $"[ID: {discount.DiscountedProductId}] " +
                    $"Pris: {discount.DiscountPrice} eller " +
                    $"2 for 3: {discount.TwoForThree}. " +
                    $"({discount.StartDate.ToString("yyyy-MM-dd")} till " +
                    $"{discount.EndDate.ToString("yyyy-MM-dd")})");
            }

            Utility.WaitForKeyPress();
            DiscountOption.Menu();
        }
    }
}
