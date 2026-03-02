using KassaSystem___Kim.Admin.Discount.DiscountOptions;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.Discount
{
    internal static class DiscountOption
    {
        internal static void Menu()
        {
            Console.Clear();

            string prompt = "Kampanjer";
            string[] discountOptions = ["Lägg till kampanj", "Ta bort kampanj", "Gå tillbaka"];
            Menu discountMenu = new Menu(prompt, discountOptions);
            int selectedDiscountIndex = discountMenu.Run();

            switch (selectedDiscountIndex)
            {
                case 0:
                    DiscountOptionAdd.Run();
                    break;

                case 1:
                    break;

                case 2:
                    AdminOption.Menu();
                    break;
            }

        }
    }
}
