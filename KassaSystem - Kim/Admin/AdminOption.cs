using KassaSystem___Kim.Admin.AdminOptions;
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
            // Här ska man kunna lägga till, ta bort, ändra och läsa alla produkter.
            // Man ska även kunna lägga till och ta bort KAMPANJER.
            // (Eventuellt att man måste skriva in korrekt användarnamn och lösen.)
            App app = new App();

            string prompt = "Admin Verktyg";
            string[] options = [ "Lägg till produkt", "Ändra produkt",
                "Ta bort produkt", "Skriva ut produkter", "Gå tillbaka till huvudmenyn" ];

            Menu adminMenu = new Menu(prompt, options);
            int selectedIndex = adminMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    // Lägga till prod.
                    AdminOptionAdd.Run();
                    break;

                case 1:
                    AdminOptionUpdate.Run();
                    break;

                case 2:
                    // Ta bort prod.
                    Console.Clear();

                    string filePath = "../../../Products/ProductText/Products.csv";
                    List<string> lines = File.ReadAllLines(filePath).ToList();

                    string removePrompt = "Admin Verktyg";

                    List<string> removeOptionsList = new List<string>();
                    foreach (var line in lines)
                    {
                        removeOptionsList.Add(line);
                    }
                    string[] removeOptions = removeOptionsList.ToArray();

                    Menu removeMenu = new Menu(removePrompt, removeOptions);
                    int removeSelectedIndex = removeMenu.Run();

                    // Remove at selected INDEX. Not '0' though.
                    if (removeSelectedIndex != 0)
                    {

                    }


                    Utility.WaitForKeyPress();

                    break;

                case 4:
                    app.RunMainMenu();
                    break;
            }
        }
    }
}
