using System;
using System.Security.Cryptography;

class Program
{
    static void Main()
    {
        Random random = new Random();
        string playAgain;

        do
        {
            int magicNumber = random.Next(1, 101);
            int numberOfGuesses = 0;
            int userGuess; // Declare userGuess outside the loop

            Console.WriteLine("Welcome to the Number Guessing Game!");
            Console.WriteLine("I have selected a random number between 1 and 100.");
            Console.WriteLine("Try to guess it!");

            do
            {
                Console.Write("What is your guess? ");
                userGuess = int.Parse(Console.ReadLine()); // Assign userGuess within the loop
                numberOfGuesses++;

                if (userGuess < magicNumber)
                {
                    Console.WriteLine("Higher");
                }
                else if (userGuess > magicNumber)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                }
            } while (magicNumber != userGuess);

            Console.WriteLine($"It took you {numberOfGuesses} guesses to find the magic number.");
            Console.Write("Do you want to play again? (yes/no): ");
            playAgain = Console.ReadLine();

        } while (playAgain.ToLower() == "yes");

        Console.WriteLine("Thanks for playing! Goodbye!");
    }
}