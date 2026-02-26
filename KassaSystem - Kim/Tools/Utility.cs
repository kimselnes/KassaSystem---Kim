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
            //int pId = 0;
            //int amount = 0;
            int[] pIdAmount = new int[2];
            string[] tempStrings = new string[2];
            //string tempStr0;
            //string tempStr1;
            tempStrings = command.Split(" ");

            //foreach (var item in tempStrings)
            //{
            //    pIdAmount = Int32.Parse(item);
            //}

            for (int i = 0; i <= 1; i++)
            {
                //pIdAmount[i] = Convert.ToInt32(tempStrings[i]); 
                //pIdAmount[i] = Int32.Parse(tempStrings[i]);
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
                        pIdAmount[i] = parsed;                    }

                }


            }


            return pIdAmount;
        }
    }
}
