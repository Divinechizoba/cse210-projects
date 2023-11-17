using System;
using System.Collections.Generic;
using System.IO;

class JournalProgram
{
    private List<JournalEntries> entries = new List<JournalEntries>();
    private List<string> prompts = new List<string>
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "Did you go out today?",
        "WHo made your day very special or unbearable?",
        "How was the traffic situation in your city today?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?"
    };

    public void WriteNewEntry()
    {
        Console.WriteLine("New Entry:");
        string randomPrompt = GetRandomPrompt();
        Console.WriteLine($"Prompt: {randomPrompt}");
        Console.Write("Response: ");
        string response = Console.ReadLine();
        string date = DateTime.Now.ToString("yyyy-MM-dd");

        JournalEntries entry = new JournalEntries
        {
            Prompt = randomPrompt,
            Response = response,
            Date = date
        };

        entries.Add(entry);
        Console.WriteLine("Entry saved successfully.");
    }

    public void DisplayJournal()
    {
        Console.WriteLine("Journal Entries:");
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}\n");
        }
    }

    public void SaveToFileSync()
    {
        Console.Write("Enter a filename to save the journal (without extension): ");
        string filename = Console.ReadLine() + ".csv";

        using (StreamWriter writer = new StreamWriter(filename))
        {
            // Write the header line
            writer.WriteLine("Date,Prompt,Response");

            foreach (var entry in entries)
            {
                // Use quotes to handle commas in responses
                writer.WriteLine($"{entry.Date}," +
                                 $"\"{entry.Prompt.Replace("\"", "\"\"")}\"," +
                                 $"\"{entry.Response.Replace("\"", "\"\"")}\"");
            }
        }

        Console.WriteLine("Journal saved to file successfully.");
    }

    public void LoadFromFile()
    {
        Console.Write("Enter a filename to load the journal (including extension): ");
        string filename = Console.ReadLine();

        if (File.Exists(filename))
        {
            entries.Clear();

            using (StreamReader reader = new StreamReader(filename))
            {
                // Read the header line and discard it
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    string[] parts = reader.ReadLine().Split(',');

                    JournalEntries entry = new JournalEntries
                    {
                        Date = parts[0],
                        // Remove quotes and handle double quotes inside prompts and responses
                        Prompt = parts[1].Trim('"').Replace("\"\"", "\""),
                        Response = parts[2].Trim('"').Replace("\"\"", "\"")
                    };

                    entries.Add(entry);
                }
            }

            Console.WriteLine("Journal loaded from file successfully.");
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }

    private string GetRandomPrompt()
    {
        Random random = new Random();
        int index = random.Next(prompts.Count);
        return prompts[index];
    }

    static void Main(string[] args)
    {
        JournalProgram journalProgram = new JournalProgram();

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    journalProgram.WriteNewEntry();
                    break;
                case "2":
                    journalProgram.DisplayJournal();
                    break;
                case "3":
                    journalProgram.SaveToFileSync();
                    break;
                case "4":
                    journalProgram.LoadFromFile();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}


// I made my code, able to handle csv files, putting special symbols into consideration. 