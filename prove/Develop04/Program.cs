using System;
using System.Threading;

public abstract class MindfulnessActivity
{
    protected int DurationInSeconds;

    public MindfulnessActivity(int duration)
    {
        DurationInSeconds = duration;
    }

    public abstract void StartActivity();

    protected virtual void DisplayStartingMessage(string activityName, string description)
    {
        Console.WriteLine($"=== {activityName} ===");
        Console.WriteLine(description);
        Console.WriteLine($"Duration: {DurationInSeconds} seconds");
        Console.WriteLine("Prepare to begin...");
        Thread.Sleep(3000);
    }

    protected virtual void DisplayEndingMessage(string activityName)
    {
        Console.WriteLine($"Congratulations! You've completed the {activityName}.");
        Console.WriteLine($"Time spent: {DurationInSeconds} seconds");
        Thread.Sleep(3000);
    }

    protected virtual void ShowAnimation()
    {
        // Placeholder for animation logic
        Console.WriteLine("Showing animation...");
        Thread.Sleep(1000);
    }
}

public class BreathingActivity : MindfulnessActivity
{
    public BreathingActivity(int duration) : base(duration) { }

    public override void StartActivity()
    {
        DisplayStartingMessage("Breathing Activity", "This activity will help you relax by guiding you through breathing exercises.");

        int breathDuration = DurationInSeconds / 2; // Splitting the duration for inhale and exhale

        for (int i = 0; i < breathDuration; i++)
        {
            Console.WriteLine("Breathe in...");
            ShowAnimation();
        }

        for (int i = 0; i < breathDuration; i++)
        {
            Console.WriteLine("Breathe out...");
            ShowAnimation();
        }

        DisplayEndingMessage("Breathing Activity");
    }
}

public class ReflectionActivity : MindfulnessActivity
{
    private readonly string[] reflectionPrompts = {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless."
    };

    private readonly string[] reflectionQuestions = {
        "Why was this experience meaningful to you?",
        "Have you ever done anything like this before?",
        // ... other questions
    };

    public ReflectionActivity(int duration) : base(duration) { }

    public override void StartActivity()
    {
        DisplayStartingMessage("Reflection Activity", "This activity will help you reflect on past experiences.");

        Random random = new Random();
        string randomPrompt = reflectionPrompts[random.Next(reflectionPrompts.Length)];

        Console.WriteLine(randomPrompt);
        Thread.Sleep(2000); // Pause before showing questions

        foreach (string question in reflectionQuestions)
        {
            Console.WriteLine(question);
            ShowAnimation();
            Thread.Sleep(2000); // Pause between questions
        }

        DisplayEndingMessage("Reflection Activity");
    }
}

public class ListingActivity : MindfulnessActivity
{
    private readonly string[] listingPrompts = {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        // ... other prompts
    };

    public ListingActivity(int duration) : base(duration) { }

    public override void StartActivity()
    {
        DisplayStartingMessage("Listing Activity", "This activity will help you list positive aspects of your life.");

        Random random = new Random();
        string randomPrompt = listingPrompts[random.Next(listingPrompts.Length)];

        Console.WriteLine($"Think about: {randomPrompt}");
        Thread.Sleep(3000); // Pause for thinking

        int itemsListed = 0;
        while (DurationInSeconds > 0)
        {
            Console.WriteLine("Enter an item (or 'done' to finish listing):");
            string input = Console.ReadLine();
            if (input.ToLower() == "done")
                break;

            itemsListed++;
            DurationInSeconds -= 10; // Subtract time for each listed item
        }

        Console.WriteLine($"You listed {itemsListed} items.");
        DisplayEndingMessage("Listing Activity");
    }
}

class Program
{
    static void Main()
    {
        int durationInSeconds = 60;

        MindfulnessActivity breathingActivity = new BreathingActivity(durationInSeconds);
        MindfulnessActivity reflectionActivity = new ReflectionActivity(durationInSeconds);
        MindfulnessActivity listingActivity = new ListingActivity(durationInSeconds);

        Console.WriteLine("Choose an activity:");
        Console.WriteLine("1. Breathing Activity");
        Console.WriteLine("2. Reflection Activity");
        Console.WriteLine("3. Listing Activity");
        Console.Write("Enter your choice: ");
        int choice = Convert.ToInt32(Console.ReadLine());

        switch (choice)
        {
            case 1:
                breathingActivity.StartActivity();
                break;
            case 2:
                reflectionActivity.StartActivity();
                break;
            case 3:
                listingActivity.StartActivity();
                break;
            default:
                Console.WriteLine("Invalid choice!");
                break;
        }
    }
}
