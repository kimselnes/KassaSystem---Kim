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

        public static int[] SeparateCommand(string command)
        {
            // EX: "300 4"
            int[] pIdAmount = new int[2];
            string[] tempStrings = new string[2];
            tempStrings = command.Split(" ");

            for (int i = 0; i <= 1; i++)
            {
                if (tempStrings.Length != 2)
                {
                    Console.WriteLine("Felaktigt kommand.");
                    Utility.WaitForKeyPress();
                    break;
                }
                else
                {
                    if (!Int32.TryParse(tempStrings[i], out int parsed))
                    {
                        Console.WriteLine("Felaktigt ID eller ANTAL.");
                        Utility.WaitForKeyPress();
                        break;
                    }
                    else
                    {
                        pIdAmount[i] = parsed;
                    }
                }
            }

            return pIdAmount;
        }
    }
}
