using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.Discount.DiscountOptions
{
    internal class DiscountOptionDelete
    {

        public static void Run()
        {
            Console.Clear();

            List<Discount> discountList = DiscountService.SortIntoList();
            List<string> tempDeleteOptions = new List<string>();

            string discountDeletePrompt = "Ta bort kampanj";

            foreach (var item in discountList)
            {
                var tempDiscount = $"" +
                    $"{item.DiscountedProductId}, " +
                    $"2for3: {item.TwoForThree} " +
                    $"({item.StartDate.ToString("yyyy/MM/dd")} - " +
                    $"{item.EndDate.ToString("yyyy/MM/dd")})";

                tempDeleteOptions.Add(tempDiscount);
            }
            string[] deleteOptions = tempDeleteOptions.ToArray();

            Menu deleteListed = new Menu(discountDeletePrompt, deleteOptions);
            int deleteSelectedIndex = deleteListed.Run();


            string yesNoPrompt = $"\nÄr du säker på att du vill ta bort kampanjen för: " +
                $"{discountList[deleteSelectedIndex].DiscountedProductId}";
            string[] yesNoOptions = ["Ja", "Nej"];

            Menu yesNoMenu = new Menu(yesNoPrompt, yesNoOptions);
            int yesNoSelectedIndex = yesNoMenu.Run();

            switch (yesNoSelectedIndex)
            {
                case 0:
                    discountList.RemoveAt(deleteSelectedIndex);

                    DiscountService.RewriteDiscountText(discountList);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nDu har tagit bort kampanjen.");
                    Console.ResetColor();

                    Utility.WaitForKeyPress();
                    DiscountOption.Menu();
                    break;
                case 1:
                    DiscountOption.Menu();
                    break;
            }

        }
    }
}
