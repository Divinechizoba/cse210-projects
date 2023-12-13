using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
public abstract class Goal
{
    public string Description { get; set; }
    public int Points { get; protected set; }
    public bool IsCompleted { get; set; } // Change accessibility to public

    public abstract void MarkCompleted();
    public abstract string GetGoalStatus();
}

// Simple goal class
public class SimpleGoal : Goal
{
    public SimpleGoal(string description, int points)
    {
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    public override void MarkCompleted()
    {
        IsCompleted = true;
    }

    public override string GetGoalStatus()
    {
        return IsCompleted ? "[X]" : "[ ]";
    }
}

// Eternal goal class
public class EternalGoal : Goal
{
    public EternalGoal(string description, int points)
    {
        Description = description;
        Points = points;
        IsCompleted = false;
    }

    public override void MarkCompleted()
    {
        Points += 100; // Example: Increment points for each completion
    }

    public override string GetGoalStatus()
    {
        return $"Completed {Points} times";
    }
}

// Checklist goal class
public class ChecklistGoal : Goal
{
    private int completionTarget;
    private int completions;

    public ChecklistGoal(string description, int points, int target)
    {
        Description = description;
        Points = points;
        IsCompleted = false;
        completionTarget = target;
        completions = 0;
    }

    public override void MarkCompleted()
    {
        completions++;
        if (completions >= completionTarget)
        {
            IsCompleted = true;
            Points += 500; // Example: Bonus points upon completion
        }
    }

    public override string GetGoalStatus()
    {
        return $"Completed {completions}/{completionTarget} times";
    }
}

// Your main program logic goes here (creating goals, managing user interactions, etc.)
class Program
{
    static List<Goal> goals = new List<Goal>(); // Store user's goals
    const string saveFileName = "goals.txt"; // File to save/load data

    static void Main(string[] args)
    {
        LoadGoals(); // Load existing goals from file, if any

        Console.WriteLine("Welcome to Eternal Quest Program!");

        bool exit = false;
        while (!exit)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateNewGoal();
                    break;
                case "2":
                    RecordEvent();
                    break;
                case "3":
                    DisplayGoals();
                    break;
                case "4":
                    DisplayScore();
                    break;
                case "5":
                    SaveGoals();
                    break;
                case "6":
                    LoadGoals();
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }

        Console.WriteLine("Goodbye!");
    }

    static void DisplayMenu()
    {
        Console.WriteLine("\nMenu:");
        Console.WriteLine("1. Create New Goal");
        Console.WriteLine("2. Record Event (Mark Goal Completed)");
        Console.WriteLine("3. Display Goals");
        Console.WriteLine("4. Display Score");
        Console.WriteLine("5. Save Goals");
        Console.WriteLine("6. Load Goals");
        Console.WriteLine("7. Exit");
        Console.Write("Enter your choice: ");
    }

    static void CreateNewGoal()
    {
        Console.WriteLine("\nEnter goal details:");
        Console.Write("Description: ");
        string description = Console.ReadLine();
        Console.Write("Type (1 - Simple, 2 - Eternal, 3 - Checklist): ");
        string typeChoice = Console.ReadLine();

        switch (typeChoice)
        {
            case "1":
                goals.Add(new SimpleGoal(description, 100)); // 100 is example points
                break;
            case "2":
                goals.Add(new EternalGoal(description, 50)); // 50 is example points
                break;
            case "3":
                Console.Write("Completion Target: ");
                int completionTarget;
                if (int.TryParse(Console.ReadLine(), out completionTarget))
                {
                    goals.Add(new ChecklistGoal(description, 30, completionTarget)); // 30 is example points
                }
                else
                {
                    Console.WriteLine("Invalid input. Checklist goal not created.");
                }
                break;
            default:
                Console.WriteLine("Invalid choice. Goal not created.");
                break;
        }
    }

    static void RecordEvent()
    {
        Console.WriteLine("\nSelect goal to mark as completed:");
        DisplayGoals();

        Console.Write("Enter goal number to mark as completed: ");
        if (int.TryParse(Console.ReadLine(), out int goalIndex) && goalIndex >= 0 && goalIndex < goals.Count
                )
        {
            goals[goalIndex].MarkCompleted();
            Console.WriteLine("Event recorded. Goal marked as completed.");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    static void DisplayGoals()
    {
        Console.WriteLine("\nGoals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i}. {goals[i].Description} - {goals[i].GetGoalStatus()}");
        }
    }

    static void DisplayScore()
    {
        int totalScore = 0;
        foreach (var goal in goals)
        {
            totalScore += goal.Points;
        }
        Console.WriteLine($"\nTotal Score: {totalScore}");
    }

    static void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter(saveFileName))
        {
            foreach (var goal in goals)
            {
                writer.WriteLine($"{goal.GetType().Name}:{goal.Description}:{goal.Points}:{goal.IsCompleted}");
            }
        }
        Console.WriteLine("Goals saved to file.");
    }

    static void LoadGoals()
    {
        if (File.Exists(saveFileName))
        {
            goals.Clear();
            string[] lines = File.ReadAllLines(saveFileName);
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length >= 4)
                {
                    string goalType = parts[0];
                    string description = parts[1];
                    int points;
                    if (int.TryParse(parts[2], out points))
                    {
                        bool isCompleted = bool.Parse(parts[3]);

                        // Based on goal type, create the appropriate goal object and add it to the list
                        switch (goalType)
                        {
                            case nameof(SimpleGoal):
                                goals.Add(new SimpleGoal(description, points) { IsCompleted = isCompleted });
                                break;
                            case nameof(EternalGoal):
                                goals.Add(new EternalGoal(description, points) { IsCompleted = isCompleted });
                                break;
                            case nameof(ChecklistGoal):
                                int completionTarget = parts.Length >= 5 ? int.Parse(parts[4]) : 0;
                                goals.Add(new ChecklistGoal(description, points, completionTarget) { IsCompleted = isCompleted });
                                break;
                            default:
                                Console.WriteLine("Unknown goal type in file.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid points for goal in file.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid format for goal in file.");
                }
            }
            Console.WriteLine("Goals loaded from file.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}
