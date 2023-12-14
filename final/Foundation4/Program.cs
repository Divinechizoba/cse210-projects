using System;
using System.Collections.Generic;

// Base class for exercise activities
class Activity
{
    // Common activity attributes
    public DateTime Date { get; set; }
    public int LengthInMinutes { get; set; }

    // Method to calculate distance (to be overridden by derived classes)
    public virtual double CalculateDistance()
    {
        return 0;
    }

    // Method to calculate speed (to be overridden by derived classes)
    public virtual double CalculateSpeed()
    {
        return 0;
    }

    // Method to calculate pace (to be overridden by derived classes)
    public virtual double CalculatePace()
    {
        return 0;
    }

    // Method to generate summary of the activity
    public virtual void GetSummary()
    {
        Console.WriteLine($"{Date.ToShortDateString()} {GetType().Name} ({LengthInMinutes} min)");
        Console.WriteLine($"Distance: {CalculateDistance()} miles");
        Console.WriteLine($"Speed: {CalculateSpeed()} mph");
        Console.WriteLine($"Pace: {CalculatePace()} min per mile");
    }
}

// Derived class for Running activity
class Running : Activity
{
    // Additional attribute for running
    public double Distance { get; set; }

    // Override method to calculate distance for running
    public override double CalculateDistance()
    {
        return Distance;
    }

    // Override method to calculate speed for running
    public override double CalculateSpeed()
    {
        return Distance / (LengthInMinutes / 60.0);
    }

    // Override method to calculate pace for running
    public override double CalculatePace()
    {
        return LengthInMinutes / Distance;
    }
}

// Derived class for Cycling activity
class Cycling : Activity
{
    // Additional attribute for cycling
    public double Speed { get; set; }

    // Override method to calculate distance for cycling
    public override double CalculateDistance()
    {
        return Speed * (LengthInMinutes / 60.0);
    }

    // Override method to calculate pace for cycling
    public override double CalculatePace()
    {
        return 60.0 / Speed;
    }
}

// Derived class for Swimming activity
class Swimming : Activity
{
    // Additional attribute for swimming
    public int Laps { get; set; }

    // Override method to calculate distance for swimming
    public override double CalculateDistance()
    {
        return Laps * 0.03125; // 1 lap is approximately 50 meters (converted to miles)
    }

    // Override method to calculate speed for swimming
    public override double CalculateSpeed()
    {
        return CalculateDistance() / (LengthInMinutes / 60.0);
    }

    // Override method to calculate pace for swimming
    public override double CalculatePace()
    {
        return LengthInMinutes / CalculateDistance();
    }
}

class Program
{
    static void Main()
    {
        // Creating instances of different exercise activities
        Running runningActivity = new Running
        {
            Date = DateTime.Now,
            LengthInMinutes = 30,
            Distance = 3.5 // in miles
        };

        Cycling cyclingActivity = new Cycling
        {
            Date = DateTime.Now.AddDays(-1),
            LengthInMinutes = 45,
            Speed = 15 // in mph
        };

        Swimming swimmingActivity = new Swimming
        {
            Date = DateTime.Now.AddDays(-2),
            LengthInMinutes = 60,
            Laps = 20
        };

        // Displaying activity summaries
        Console.WriteLine("Exercise Activity Summaries:");
        runningActivity.GetSummary();
        Console.WriteLine();
        cyclingActivity.GetSummary();
        Console.WriteLine();
        swimmingActivity.GetSummary();
    }
}
