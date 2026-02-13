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

    }
}
