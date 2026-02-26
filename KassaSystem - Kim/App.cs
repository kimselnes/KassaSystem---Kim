using System;
using System.Collections.Generic;
using System.Text;
using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using KassaSystem___Kim.Receipt;

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


        public void RunMainMenu()
        {
            string prompt = "Kassa System";
            string[] options = ["Ny kund", "Admin meny", "Avsluta", "TEST"]; // TA BORT TEST.

            Menu mainMenu = new Menu(prompt, options);
            int selectedIndex = mainMenu.Run();

            switch (selectedIndex)
            {
                case 0:
                    Register();
                    //Utility.WaitForKeyPress();
                    //RunMainMenu();
                    break;

                case 1:
                    AdminMenu();
                    break;

                case 2:
                    Utility.ExitApp();
                    break;
                case 3:
                    // Här TESTAR jag klasser/metoder under utvecklingen. TA BORT!!

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

        //private void Register()
        //{
        //    // Kassan
        //    // Här sker betalningen.
        //    // Om man matar in <productId> och <amount> som "300 4" - (MÅSTE SKRIVAS PÅ DETTA VIS OCH HA MELLANSLAG)
        //    // behöver man separera numre(r)na  och identifiera de som productId och amount.
        //    // Typ lägga i en array och se [0] som productId och [1] som amount.
        //    var receipt = new Receipts();
        //    var registerProducts = new List<Product>();
        //    var foundProduct = new Product();
        //    int[] separatedCommand;
        //    bool notPayed = true;

        //    while (notPayed)
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Kassa\n");

        //        receipt.PrintToScreen(registerProducts, foundProduct.Amount);

        //        // UserINPUT
        //        Console.WriteLine("\nKommandon:");
        //        Console.WriteLine("<produktId> <Antal>");
        //        Console.WriteLine("PAY\n");
        //        Console.Write("Kommando:");
        //        var userInput = Console.ReadLine();

        //        if (userInput == "PAY")
        //        {
        //            Receipts receipts = new Receipts();
        //            receipts.AddReceiptToFile(registerProducts, foundProduct.Amount);
        //            notPayed = false;
        //        }
        //        else if (true)
        //        {
        //            // Separate ID and AMOUNT
        //            separatedCommand = Utility.SeparateCommand(userInput);

        //            foundProduct = Receipts.FindProduct(separatedCommand[0]);
        //            if (foundProduct.ProductName != null)
        //            {
        //                foundProduct.Amount = separatedCommand[1];
        //                registerProducts.Add(foundProduct);
        //            }
        //            else
        //            {

        //            }

        //        }
        //        //receipt.PrintToScreen(foundProduct, separatedCommand[1]);
        //    }



        //Console.WriteLine();
        //Utility.WaitForKeyPress();
        //RunMainMenu();
        //}
        private void Register() // KOPIA, SKA SEI FALL JAG KAN LÖSA AMOUNT ISSUE.
        {
            // Kassan
            // Här sker betalningen.
            // Om man matar in <productId> och <amount> som "300 4" - (MÅSTE SKRIVAS PÅ DETTA VIS OCH HA MELLANSLAG)
            // behöver man separera numre(r)na  och identifiera de som productId och amount.
            // Typ lägga i en array och se [0] som productId och [1] som amount.
            var receipt = new Receipts();
            var registerProducts = new List<Product>();
            var foundProduct = new Product();
            int[] separatedCommand;
            bool notPayed = true;

            while (notPayed)
            {
                Console.Clear();
                Console.WriteLine("Kassa\n");

                receipt.PrintToScreen(registerProducts);

                // UserINPUT
                Console.WriteLine("\nKommandon:");
                Console.WriteLine("<produktId> <Antal>");
                Console.WriteLine("PAY\n");
                Console.Write("Kommando:");
                var userInput = Console.ReadLine();

                if (userInput.ToUpper() == "PAY")
                {
                    Receipts receipts = new Receipts();
                    receipts.AddReceiptToFile(registerProducts);
                    notPayed = false;
                }
                else if (true)
                {
                    // Separate ID and AMOUNT
                    separatedCommand = Utility.SeparateCommand(userInput);

                    foundProduct = Receipts.FindProduct(separatedCommand[0]);

                    if (foundProduct.ProductName != null)
                    {
                        foundProduct.Amount = separatedCommand[1];
                        registerProducts.Add(foundProduct);
                        receipt.AddTotalPrice(foundProduct);
                    }
                }
            }

            Console.WriteLine();
            Utility.WaitForKeyPress();
            RunMainMenu();
        }

        public void AdminMenu()
        {
            // Här ska man kunna lägga till, ta bort, ändra och läsa alla produkter.
            // Man ska även kunna lägga till och ta bort KAMPANJER.
            // (Eventuellt att man måste skriva in korrekt användarnamn och lösen.)

            string prompt = "Admin Verktyg";
            string[] options = [ "Lägg till produkt", "Ändra produkt",
                "Ta bort produkt", "Skriva ut produkter", "Gå tillbaka till huvudmenyn" ];

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
                    if (!Int32.TryParse(Console.ReadLine(), out var pId))
                    {
                        Console.WriteLine("Ange 3 siffrigt Produkt ID.");
                        Utility.WaitForKeyPress();
                        AdminMenu();
                    }
                    Console.Write("Produkt namn: ");
                    string pName = Console.ReadLine();
                    Console.Write("Pris: ");
                    //Decimal.TryParse(Console.ReadLine(), out var pPrice); // Se till att det inte blir fel om man använder '.' istället för ','.
                    if (!Decimal.TryParse(Console.ReadLine(), out var pPrice))
                    {
                        Console.WriteLine("Ange korrekt pris.");
                        Utility.WaitForKeyPress();
                        AdminMenu();
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

                    AdminMenu();
                    break;

                case 1:

                    Console.Clear();

                    var readProduct = new ReadProduct();
                    var sortedProducts = readProduct.SortIntoList();

                    List<string> tempListToArray = new List<string>();

                    foreach (var product in sortedProducts)
                    {
                        string tempString = $"{product.ProductId},{product.ProductName},{product.ProductPrice},{product.PriceType}";
                        tempListToArray.Add(tempString);
                    }
                    string[] updateOptions = tempListToArray.ToArray();

                    string updatePrompt = "Ändra produkter";
                    Menu updateMenu = new Menu(updatePrompt, updateOptions);

                    int updateIndex = updateMenu.Run();

                    // OM updateIndex == sortedProducts[index].

                    foreach (var product in sortedProducts)
                    {
                        for (int i = 0; i < sortedProducts.Count; i++)
                        {
                            if (updateIndex == sortedProducts.IndexOf(product, i, 1))
                            {
                                UpdateProduct.Update(product);
                            }
                        }

                    }





                    Console.WriteLine();
                    Utility.WaitForKeyPress();

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
                    RunMainMenu();
                    break;
            }



            // Ändra prod.




            // Skriva ut prods.


            // KAMANJER. TBA.


            // Avsluta/Gå tillbaka

        }
    }
}
