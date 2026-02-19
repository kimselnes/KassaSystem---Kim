using System;
using System.Collections.Generic;
using System.Text;
using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;

namespace KassaSystem___Kim
{
    internal class App
    {

        public void Start()
        {
            // if catches error do Run() again?
            // "error message"
            // WaitForKeyPress()
            // Run()

            Console.Title = "Kim's Kassasystem";

            RunMainMenu();





        }


        private void RunMainMenu()
        {
            string prompt = "Kassa System";
            string[] options = { "Ny kund", "Admin meny", "Avsluta" };

            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    NewCustomer();


                    Utility.WaitForKeyPress();
                    RunMainMenu();
                    break;

                case 1:
                    AdminMenu();
                    break;

                case 2:
                    Utility.ExitApp();
                    break;
            }
        }

        //private void ExitApp()
        //{
        //    Console.ForegroundColor = ConsoleColor.Red;
        //    Console.WriteLine("\nAvslutar Kassasystemet. Tryck valfri knapp för att avsluta.");
        //    Console.ResetColor();
        //    Console.ReadKey(true);
        //    Environment.Exit(0);
        //}

        private void NewCustomer()
        {
            // Ny försäljning.
            // Verktyg/meny för kunden att "plocka" ihop varor/produkter.
            Console.Clear();
            Console.WriteLine("Ny kund.");

            Utility.WaitForKeyPress();
            RunMainMenu();
        }

        private void Register()
        {
            // Kassan
            // Här sker betalningen.
            // Om man matar in <productId> och <amount> som "300 4" - (MÅSTE SKRIVAS PÅ DETTA VIS OCH HA MELLANSLAG)
            // behöver man separera numre(r)na  och identifiera de som productId och amount.
            // Typ lägga i en array och se [0] som productId och [1] som amount.
        }

        private void AdminMenu()
        {
            //var createProduct = new CreateProduct(300, "Bananer", 12.50m, "st");
            //var createProduct = new CreateProduct(400, "Mjölk", 15.50m, "st");
            //createProduct.AddProductToFile(createProduct);

            // Här ska man kunna lägga till, ta bort, ändra och läsa alla produkter.
            // Man ska även kunna lägga till och ta bort KAMPANJER.
            // (Eventuellt att man måste skriva in korrekt användarnamn och lösen.)

            // MENY

            string prompt = "Admin Verktyg";
            string[] options = { "Lägg till produkt", "Ändra produkt",
                "Ta bort produkt", "Skriva ut produkter", "Gå tillbaka" };

            Menu adminMenu = new Menu(prompt, options);
            int selectedIndex = adminMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    // Lägga till prod.

                    //var createProduct = new CreateProduct(300, "Bananer", 12.50m, "st");
                    Console.Clear();

                    Console.WriteLine("Lägga till produkt\n");
                    Console.Write("Podukt ID: "); // Lägga till så att produktId bara kan bara 3 siffror.
                    //int pId = Int32.Parse(Console.ReadLine());
                    Int32.TryParse(Console.ReadLine(), out var pId);
                    Console.Write("Produkt namn: ");
                    string pName = Console.ReadLine();
                    Console.Write("Pris: ");
                    Decimal.TryParse(Console.ReadLine(), out var pPrice); // Se till att det inte blir fel om man använder '.' istället för ','.
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

                    break;

                    case 1:
                    // REMOVE DOWNWARDS
                    var newProduct69 = new Product(300, "Bananer", 12.50m, "st");
                    AddProduct.AddProductToFile(newProduct69);
                    // REMOVE UPWARDS
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
            }



            // Ändra prod.




            // Skriva ut prods.


            // KAMANJER. TBA.


            // Avsluta/Gå tillbaka

        }
    }
}
