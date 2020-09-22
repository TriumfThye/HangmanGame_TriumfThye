using System;
using System.Text;
using System.Linq;

namespace HangmanGame
{
    /*  Assignment 2, Hangman. Ulf Bengtsson at Lexicon Växjö.
        Basic Console-based word game using C#.
        Thye Hansen, triumfthye@hotmail.com
        2020-09-22 
    */
    class Program
    {
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
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("\nYou can only enter \"y\" or \"n\"!\n");
                        break;
                }
            }

        }//MainChoise

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
            string usedRight = rightGuesses.ToString();
            string usedWrong = wrongGuesses.ToString();

            bool runGame = true;
            int lives = 10;

            while (runGame && lives > 0)
            {
                Console.WriteLine("Here are the attempts you have got left: {0}", lives);
                Console.WriteLine("The wrong attempts goes here to aid you: {0}", wrongGuesses);
                Console.Write("\nGuess a letter or the word ");
                Console.WriteLine(theLines);
                Console.Write("Your guess: ");

                string input = Console.ReadLine().ToUpper();
                Console.WriteLine("---------------------------------------");

                NullCheck(input, out bool goContinue);
                if (goContinue) continue;

                char guess = input[0];

                SymbolCheck(input, theWord, out goContinue);
                if (goContinue) continue;

                if (input.Length == theWord.Length)
                {
                    if (!input.SequenceEqual(theLetters))
                    {
                        Console.WriteLine("\nYou guessed a WRONG word!\n");
                        lives--;
                        continue;
                    }
                }
                else if (usedRight.Contains(input))
                {
                    Console.WriteLine("\nYou've allready tried \"{0}\", and you guessed RIGHT!\n", guess);
                    continue;
                }
                else if (usedWrong.Contains(input))
                {
                    Console.WriteLine("\nYou've allready tried \"{0}\", and your guess was WRONG!\n", guess);
                    continue;
                }
                else if (theWord.Contains(guess))
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
                    Console.Write("\nThe word \"");
                    Console.Write(theLines);
                    Console.WriteLine("\" is the right word. You WON!\n");
                    runGame = false;
                }
                else if (theLines.SequenceEqual(theLetters))//Victory
                {
                    Console.Write("The word \"");
                    Console.Write(theLines);
                    Console.WriteLine("\" is the right word. You WON!\n");
                    runGame = false;
                }
                else if (lives < 1)//Defeat
                {
                    Console.WriteLine("Too bad you ran out of lives and LOST!\n");
                    runGame = false;
                }

            }//While game

        }//RunHag

        static void NullCheck(string input, out bool goContinue)
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("\nYou DID NOT type anything!\n");
                goContinue = true;
            }
            else goContinue = false;
        } //NullCheck

        static void SymbolCheck(string input, string theWord, out bool goContinue)
        {
            bool goToContinue = true;

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] < 65 || input[i] > 90)
                {
                    Console.WriteLine("\nYou can ONLY use letters!\n");
                    goToContinue = true;
                    break;
                }
                else if (input.Length != 1 && input.Length != theWord.Length)
                {
                    Console.WriteLine("\nYou entered an UNUSABLE number of letters!\n");
                    goToContinue = true;
                    break;
                }
                else goToContinue = false;
            }

            if (goToContinue) goContinue = true;

            else goContinue = false;
        }//SymbolCheck

        static string WordChoise()
        {
            string secretWordsCollection = ("cargo, grace, greed, vault, emoji, inbox, noise, " +
                                            "exit, camp, jump, swag");
            string[] secretWords = secretWordsCollection.Split(", ");

            Random rand = new Random();

            int index = rand.Next(secretWords.Length);
            string theWordToUse = secretWords[index];
            string theWord = theWordToUse.ToUpper();
            return theWord;
        }//WordChoise



    }//class


}//namespace
