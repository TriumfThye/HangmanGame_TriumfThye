using System;
using System.Text;
using System.Linq;
//using System IO; if txt file

namespace HangmanTest
{
    /*  Assignment 2, Hangman. Ulf Bengtsson at Lexicon Växjö.
        Basic Console-based word game using C#.
        Thye Hansen, triumfthye@hotmail.com
        2020-09-20 
    */
    class Program
    {
        static void RunHang()
        {
            Console.WriteLine("\n--- The Hangman Game ---\n");

            string theWord = WordChoise();
            char[] theLetters = theWord.ToCharArray();
            char[] theLines = theWord.ToCharArray();

            for (int i = 0; i < theWord.Length; i++)
            {
                theLines[i] = '_';
            }

            StringBuilder rightGuesses = new StringBuilder(theWord.Length);
            StringBuilder wrongGuesses = new StringBuilder(theWord.Length);

            bool runGame = true;
            int lives = 10;

            string input;
            char guess;

            while (runGame && lives > 0)
            {
                string usedRight = rightGuesses.ToString();
                string usedWrong = wrongGuesses.ToString();

                Console.WriteLine("Here are the attempts you have got left: {0}", lives);//Display
                Console.WriteLine("The wrong attempts goes here to aid you: {0}", wrongGuesses);//Display
                Console.Write("\nGuess a letter or the word ");
                Console.WriteLine(theLines);//Display
                Console.Write("Your guess: ");

                input = Console.ReadLine().ToUpper();
                Console.WriteLine("---------------------------------------");

                if (string.IsNullOrEmpty(input))//Error check
                {
                    Console.WriteLine("\nYou DID NOT type anything!\n");
                    continue;
                }

                guess = input[0];

                bool checkForSymbol = false;

                for (int i = 0; i < input.Length; i++)//Error check
                {
                    if (input[i] < 65 || input[i] > 90)
                    {
                        checkForSymbol = true;
                    }
                }

                if (checkForSymbol)//Error check
                {
                    Console.WriteLine("\nYou can ONLY use letters!\n");
                    continue;
                }

                if (input.Length != 1 && input.Length != theWord.Length)//Error check
                {
                    Console.WriteLine("\nYou entered an UNUSABLE number of letters!\n");
                    continue;
                }

                if (input.Length == theWord.Length)
                {
                    if (input.SequenceEqual(theLetters))
                    { }//Clumsy but effective
                    else
                    {
                        Console.WriteLine("\nYou guessed a WRONG word!\n");
                        lives--;
                        continue;
                    }
                }

                if (usedRight.Contains(input))
                {
                    Console.WriteLine("\nYou've allready tried \"{0}\", and you guessed RIGHT!\n", guess);
                    continue;
                }
                else if (usedWrong.Contains(input))
                {
                    Console.WriteLine("\nYou've allready tried \"{0}\", and your guess was WRONG!\n", guess);
                    continue;
                }

                if (theWord.Contains(guess))
                {
                    Console.WriteLine("\nYou guessed RIGHT!\n");
                    rightGuesses.Append(guess);
                    lives--;

                    for (int i = 0; i < theWord.Length; i++)
                    {
                        if (theLetters[i] == guess)
                        {
                            theLines[i] = guess;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nYour guess is WRONG!\n");
                    wrongGuesses.Append(guess + " ");
                    lives--;
                }

                if (input.SequenceEqual(theLetters))//Victory 
                {
                    for (int i = 0; i < theWord.Length; i++)
                    {
                        theLines[i] = input[i];
                    }
                    Console.Write("The word \"");
                    Console.Write(theLines);
                    Console.WriteLine("\", is the right word. You WON!\n");
                    runGame = false;
                }
                else if (theLines.SequenceEqual(theLetters))//Victory
                {
                    Console.Write("The word \"");
                    Console.Write(theLines);
                    Console.WriteLine("\", is the right word. You WON!\n");
                    runGame = false;
                }
                else if (lives < 1)//Defeat
                {
                    Console.WriteLine("Too bad you ran out of lives and LOST!\n");
                }
            }

        }

        static string WordChoise()
        {
            //string path = @"C:\Users\Thye\Documents\wordBank.txt"; ...if txt file
            //string secretWordsCollection = File.ReadAllText(path); ...if txt file

            string secretWordsCollection = ("cargo, grace, greed, vault, emoji, inbox, noise, " +
                                            "exit, camp, jump, swag");
            string[] secretWords = secretWordsCollection.Split(", ");

            Random rand = new Random();

            int index = rand.Next(secretWords.Length);
            string theWordToUse = secretWords[index];
            string theWord = theWordToUse.ToUpper();
            return theWord;
        }

        static void Main(string[] args)
        {
            bool runChoise = true;
            while (runChoise)
            {
                Console.Write("Play the Hangman Game? y/n: ");
                string input = Console.ReadLine();
                Console.Clear();

                switch (input)
                {
                    case "y":
                        RunHang();
                        break;
                    case "n":
                        runChoise = false;
                        break;
                    default:
                        Console.WriteLine("\nYou can only enter \"y\" or \"n\"!\n");
                        break;
                }
            }

        }


    }


}
