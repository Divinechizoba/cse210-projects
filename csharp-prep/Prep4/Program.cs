using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> numbers = new List<int>();
        int num;
        bool inputFinished = false;

        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        while (!inputFinished)
        {
            Console.Write("Enter number: ");
            num = int.Parse(Console.ReadLine());

            if (num == 0)
            {
                inputFinished = true;
            }
            else
            {
                numbers.Add(num);
            }
        }

        if (numbers.Count > 0)
        {
            int sum = numbers.Sum();
            double average = numbers.Average();
            int max = numbers.Max();
            int smallestPositive = numbers.Where(n => n > 0).Min();
            List<int> sortedList = numbers.OrderBy(n => n).ToList();

            Console.WriteLine("The sum is: " + sum);
            Console.WriteLine("The average is: " + average);
            Console.WriteLine("The largest number is: " + max);
            Console.WriteLine("The smallest positive number is: " + smallestPositive);
            Console.WriteLine("The sorted list is:");
            foreach (int n in sortedList)
            {
                Console.WriteLine(n);
            }
        }
        else
        {
            Console.WriteLine("No numbers were entered.");
        }
    }
}
