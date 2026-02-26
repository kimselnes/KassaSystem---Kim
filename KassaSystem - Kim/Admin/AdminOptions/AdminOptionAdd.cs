using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.AdminOptions
{
    internal static class AdminOptionAdd
    {
        public static void Run()
        {
            Console.Clear();

            Console.WriteLine("Lägga till produkt\n");
            Console.Write("Produkt ID: "); // Lägga till så att produktId bara kan bara 3 siffror.
                                          //int pId = Int32.Parse(Console.ReadLine());
            if (!Int32.TryParse(Console.ReadLine(), out var pId))
            {
                Console.WriteLine("Ange 3 siffrigt Produkt ID.");
                Utility.WaitForKeyPress();
                AdminOption.Menu();
            }
            Console.Write("Produkt namn: ");
            string pName = Console.ReadLine();
            Console.Write("Pris: ");
            //Decimal.TryParse(Console.ReadLine(), out var pPrice); // Se till att det inte blir fel om man använder '.' istället för ','.
            if (!Decimal.TryParse(Console.ReadLine(), out var pPrice))
            {
                Console.WriteLine("Ange korrekt pris.");
                Utility.WaitForKeyPress();
                AdminOption.Menu();
            }
            Console.Write("Kilo eller Styck pris? KG/ST: ");
            string pType = Console.ReadLine(); // Lägga till ToUpper() här istället?

            var newProduct = new Product(pId, pName, pPrice, pType);
            AddProduct.AddProductToFile(newProduct);

            // If success...
            Console.WriteLine($"\nDu har lagt till:" +
                $"'{pName}'" +
                $" med ID '{pId}'" +
                $" och priset '{pPrice}{pType}'.\n");
            Utility.WaitForKeyPress();

            // SE TILL ATT ETT ID ELLLER NAMN INTE KAN FÖREKOMMA MER ÄN EN GÅNG.

            AdminOption.Menu();
        }
    }
}
