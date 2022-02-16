using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MemoryGame
{
    static class hard
    {
        static int attempts = 15;
        public static int countofWords = 8;
        static string level = "Hard";
        static List<string> words = new List<string>();
        static string[,] array = new string[2, countofWords];


        public static void ArrayWithWords(string path)
        {
            //Download words to program
            Random random = new Random();


            try
            {
                using StreamReader sr = new StreamReader($@"{path}");
                {
                    string word;
                    do
                    {
                        word = sr.ReadLine();
                        words.Add(word);
                    } while (word != null);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }


            List<string> wordsInArray = new List<string>();
            HashSet<int> hnumbersofWord = new HashSet<int>(); //unigue numbers of words 
            do
            {
                hnumbersofWord.Add(random.Next(0, words.Count));
            } while (hnumbersofWord.Count != countofWords);

            //create the table of the words
            foreach (int item in hnumbersofWord)
            {
                for (int i = 0; i < 2; i++)
                {
                    wordsInArray.Add(words[item]);
                }
            }

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < countofWords; j++)
                {
                    coordinateOfWord c = new coordinateOfWord();
                    while (true)
                    {
                        int index = random.Next(wordsInArray.Count);
                        if (wordsInArray[index] != " ")
                        {
                            c.Word = wordsInArray[index];
                            wordsInArray[index] = " ";
                            break;
                        }
                    }
                    array[i, j] = c.Word;
                }

            }
        }
        public static void Game(string pathToSaveInformation)
        {
            int rightAnswers = 0;
            int chances = 0;

            DateTime start = DateTime.Now;

            coordinateOfWord cConst = new coordinateOfWord();
            do
            {
                chances++;
                coordinateOfWord c = new coordinateOfWord();

                //first question
                Console.Write("\nChoose the first word to uncover:\t");
                string answer1 = Console.ReadLine();
                int pozy1;
                if (answer1[0] == 'a' || answer1[0] == 'A')
                    pozy1 = 0;
                else pozy1 = 1;
                int pozx1 = int.Parse(answer1[1].ToString());

                Lines();
                Console.WriteLine("\n\tLevel: " + level + $"\n\tGuess chances: {attempts}\n");
                for (int i = 0; i <= countofWords; i++)
                {
                    if (i != 0)
                    {
                        Console.Write($"{i} ");
                    }
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                        Console.Write("A ");
                    else if (i == 1)
                        Console.Write("B ");
                    for (int j = 0; j < countofWords; j++)
                    {
                        if (i == pozy1 && j == (pozx1 - 1))
                        {
                            c.Word = array[i, j];
                            Console.Write(array[i, j] + " ");

                        }
                        else if (cConst.wordsOpened.Contains(array[i, j]))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(array[i, j] + " ");
                            Console.ResetColor();
                        }
                        else
                            Console.Write("* ");
                    }
                    Console.WriteLine();
                }
                Lines();

                //second questions
                Console.Write("\nChoose the second word to uncover:\t");
                string answer2 = Console.ReadLine();
                int pozy2;
                if (answer2[0] == 'a' || answer2[0] == 'A')
                    pozy2 = 0;
                else pozy2 = 1;
                int pozx2 = int.Parse(answer2[1].ToString());

                Lines();
                Console.WriteLine("\n\tLevel: " + level + $"\n\tGuess chances: {attempts}\n");
                for (int i = 0; i <= countofWords; i++)
                {
                    if (i != 0)
                    {
                        Console.Write($"{i} ");
                    }
                    else
                        Console.Write("  ");
                }
                Console.WriteLine();
                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                        Console.Write("A ");
                    else if (i == 1)
                        Console.Write("B ");
                    for (int j = 0; j < countofWords; j++)
                    {
                        if (i == pozy2 && j == (pozx2 - 1))
                        {
                            Console.Write(array[i, j] + " ");
                            if (c.Word == array[i, j])
                            {
                                cConst.wordsOpened.Add(c.Word);
                                cConst.wordsOpened.Add(array[i, j]);
                                rightAnswers++;
                            }
                            else
                            {
                                attempts--;
                                continue;
                            }
                        }
                        else if (i == pozy1 && j == (pozx1 - 1))
                            Console.Write(c.Word + " ");
                        else if (cConst.wordsOpened.Contains(array[i, j]))
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGreen;
                            Console.Write(array[i, j] + " ");
                            Console.ResetColor();
                        }
                        else
                            Console.Write("* ");

                    }
                    Console.WriteLine();
                }
                Lines();

            } while (rightAnswers != countofWords || attempts < 0);
            DateTime stop = DateTime.Now;
            if (rightAnswers == countofWords)
            {
                Console.Write("\nCongratulations! You won the game ");
                Console.OutputEncoding = System.Text.Encoding.Unicode;
                char smile = '\u263a';
                Console.Write(smile.ToString());
                Console.WriteLine($"\nYou solved the memory game after {chances} chances. It took you {Math.Round((stop - start).TotalSeconds, 1)} seconds\n");

                Console.Write("Please provide yuour name: ");
                string name = Console.ReadLine();

                //saving game result
                try
                {
                    File.WriteAllText($@"{pathToSaveInformation}", $"{name}|{DateTime.Today.Date}|{Math.Round((stop - start).TotalSeconds, 1)}|{chances}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Environment.Exit(1);
                }
            }
            else if (attempts < 0)
            {
                Console.WriteLine("\nUnfortunately you used all your attempts. \n\tSorry to inform that, but you lost :(\n");
            }

            Console.WriteLine("Best results:\n");
            //showing the best results
            int lines = 0;
            try
            {
                using StreamReader sr = new StreamReader($@"{pathToSaveInformation}");
                string line;
                {
                    do
                    {
                        line = sr.ReadLine();
                        if (line != null)
                        {
                            lines++;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            string[] information = line.Split('|');
                            Console.WriteLine($"{lines}. Name: {information[0]}\tDate: {information[1]}\tGuesiing time: {information[2]}\tGuessing tries: {information[3]}");
                            Console.ResetColor();
                        }

                    } while (line != null);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can't read the best results");
                Environment.Exit(1);
            }
        }
        public static void Lines()
        {
            for (int i = 0; i < 16; i++)
            {
                Console.Write("_ ");
            }
            Console.WriteLine();
        }
    }
}
