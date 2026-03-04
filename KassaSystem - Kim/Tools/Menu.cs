using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Tools
{
    public class Menu
    {
        // USER TEMPLATE

        //string prompt = "Your prompt";
        //string[] options = ["option1", "option2", "option3"];

        //Menu menu = new Menu(prompt, options);
        //int selectedIndex = menu.Run();

        // And then use switch(selectedIndex) as an example.
        // ---------------------------------------------------------------------

        private int SelectedIndex { get; set; }
        private string[] Options { get; set; }
        private string Prompt { get; set; }
        //private Object Object { get; set; }
        public Menu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
            //Object = @object;
        }

        private void DisplayOptions()
        {
            Console.WriteLine(Prompt);
            Console.WriteLine();

            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                //string prefix;

                if (i == SelectedIndex)
                {
                    //prefix = ">";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Gray;
                }
                else
                {
                    //prefix = " ";
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                //Console.WriteLine($"{prefix}{currentOption}");
                Console.WriteLine($"  {currentOption}  ");
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.Clear();
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = Options.Length - 1;
                    }
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                    {
                        SelectedIndex = 0;
                    }
                }
                //else if (keyPressed == ConsoleKey.Escape)
                //{
                //    return -1;
                //}
            }
            while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }
    }
}
