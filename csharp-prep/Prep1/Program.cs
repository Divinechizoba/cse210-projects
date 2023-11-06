using System;

class Program
{
    static void Main()
    {
        // user first name 
        Console.Write("Enter your first name: ");
        string firstName = Console.ReadLine();

        // user last name 
        Console.Write("Enter your last name: ");
        string lastName = Console.ReadLine();

        // display full name in this format.
        Console.WriteLine($"Your name is {lastName}, {firstName} {lastName}.");
    }
}