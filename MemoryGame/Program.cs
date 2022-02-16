using System;
using System.Collections.Generic;
using System.IO;

namespace MemoryGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //path to file with words
            Console.Write("Please provide path to words\t");
            string path = Console.ReadLine();
            
            //path to file to save information about the game
            Console.Write("Please provide the path to file where you want your attempt to be saved.");
            string pathToSave = Console.ReadLine();

        //choose a level
        setLevel:;
            Console.Write("Please provide the difficulty level. You can choose between hard and easy level: ");
            string level = Console.ReadLine();
        
            if (level.ToLower() == "easy")
            {
                easy.ArrayWithWords(path);
                easy.Game(pathToSave);
            }
            else if (level.ToLower() == "hard")
            {
                hard.ArrayWithWords(path);
                hard.Game(pathToSave);
            }
            else
            {
                int repeat = 0;
                repeat++;
                Console.WriteLine("Unfortunately, you provided incorrect name of level. Please try again :)");
                if (repeat <= 3)
                {
                    goto setLevel;
                }
                else
                {
                    Console.WriteLine("Do you want to play the game :)");
                    if (Console.ReadLine() == "Y" || Console.ReadLine() == "y")
                    {
                        goto setLevel;
                    }                        
                }
               
            }

            Console.Write("Do you wannt to play from begining?\t");
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes")
            {
                goto setLevel;
            }

            else
            {
                Console.WriteLine("Thank you for your participant");
                Environment.Exit(0);
            }

        }
    }
}
