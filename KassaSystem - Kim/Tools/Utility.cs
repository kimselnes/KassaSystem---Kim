using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Tools
{
    static class Utility
    {
        public static void WaitForKeyPress()
        {
            Console.WriteLine("Tryck valfri knapp för att fortsätta.");
            Console.ReadKey(true);
        }
        public static void ExitApp()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nAvslutar Kassasystemet. Tryck valfri knapp för att avsluta.");
            Console.ResetColor();
            Console.ReadKey(true);
            Environment.Exit(0);
        }

    }
}
