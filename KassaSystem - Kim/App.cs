using System;
using System.Collections.Generic;
using System.Text;
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
                    ExitApp();
                    break;
            }
        }

        private void ExitApp()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAvslutar Kassasystemet. Tryck valfri knapp för att avsluta.");
            Console.ResetColor();
            Console.ReadKey(true);
            Environment.Exit(0);
        }

        private void NewCustomer()
        {
            Console.WriteLine("Ny kund.");
        }

        private void AdminMenu()
        {
            Console.WriteLine("Admin meny.");
        }
    }
}
