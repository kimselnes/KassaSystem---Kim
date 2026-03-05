using KassaSystem___Kim.Admin.Discount;
using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.AdminOptions
{
    internal static class AdminOptionDelete
    {
        public static void Run()
        {
            Console.Clear();
            List<Product> sortedList = new List<Product>();
            ReadProduct readProduct = new ReadProduct();
            sortedList = readProduct.SortIntoList();
            List<string> tempDeleteOptions = new List<string>();
            string tempProduct = "";

            string prompt = "Ta bort produkt";

            foreach (var item in sortedList)
            {
                tempProduct = $"{item.ProductId} {item.ProductName}";
                tempDeleteOptions.Add(tempProduct);
            }
            string[] deleteOptions = tempDeleteOptions.ToArray();

            Menu deleteListed = new Menu(prompt, deleteOptions);
            int deleteSelectedIndex = deleteListed.Run();

            string yesNoPrompt = $"\nÄr du säker på att du vill ta bort:\n" +
                $"{tempProduct}";
            string[] yesNoOptions = ["Ja", "Nej"];

            Menu yesNoMenu = new Menu(yesNoPrompt, yesNoOptions);
            int yesNoSelectedIndex = yesNoMenu.Run();

            switch (yesNoSelectedIndex)
            {
                case 0:
                    sortedList.RemoveAt(deleteSelectedIndex);
                    UpdateProduct.RewriteProductsText(sortedList);

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($"\nDu har tagit bort produkten ifrån listan.");
                    Console.ResetColor();
                    Utility.WaitForKeyPress();
                    break;
                case 1:
                    AdminOption.Menu();
                    break;
            }
        }
    }
}
