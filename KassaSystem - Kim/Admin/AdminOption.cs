using KassaSystem___Kim.Admin.AdminOptions;
using KassaSystem___Kim.Admin.Discount;
using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin
{
    internal static class AdminOption
    {
        public static void Menu()
        {
            App app = new App();

            string prompt = "Admin Verktyg";
            string[] options = [ "Lägg till produkt", "Ändra produkt",
                "Ta bort produkt", "Skriva ut produkter", "Kampanjer", "Gå tillbaka till huvudmenyn" ];

            Menu adminMenu = new Menu(prompt, options);
            int selectedIndex = adminMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    AdminOptionAdd.Run();
                    app.RunMainMenu();
                    break;

                case 1:
                    AdminOptionUpdate.Run();
                    app.RunMainMenu();
                    break;

                case 2:
                    AdminOptionDelete.Run();
                    Menu();
                    Utility.WaitForKeyPress();
                    break;

                case 3:
                    AdminOptionRead.Run();
                    break;

                case 4:
                    DiscountOption.Menu();
                    break;

                case 5:
                    app.RunMainMenu();
                    break;
            }
        }
    }
}
