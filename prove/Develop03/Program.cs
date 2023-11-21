using System;
using System.Collections.Generic;
using System.Linq;

public class Word
{
    private string text;
    private bool hidden;

    public Word(string text)
    {
        this.text = text;
        hidden = false;
    }

    public bool IsHidden()
    {
        return hidden;
    }

    public void Hide()
    {
        hidden = true;
    }

    public string GetText()
    {
        return hidden ? "___" : text;
    }
}

public class ScriptureReference
{
    private string book;
    private int chapter;
    private int startVerse;
    private int endVerse;

    public ScriptureReference(string book, int chapter, int startVerse, int endVerse)
    {
        this.book = book;
        this.chapter = chapter;
        this.startVerse = startVerse;
        this.endVerse = endVerse;
    }

    public string GetReference()
    {
        if (startVerse == endVerse)
        {
            return $"{book} {chapter}:{startVerse}";
        }
        else
        {
            return $"{book} {chapter}:{startVerse}-{endVerse}";
        }
    }
}

public class Scripture
{
    private ScriptureReference reference;
    private List<Word> words;
    private int wordsLeft;

    public Scripture(string referenceText, string scriptureText)
    {
        // Parse referenceText and create ScriptureReference object

        // For simplicity, splitting by space assumes each word is separated by a space
        string[] wordsArray = scriptureText.Split(' ');

        // Initialize words List by creating Word objects
        words = wordsArray.Select(word => new Word(word)).ToList();

        // Sample parsing for the reference (e.g., "John 3:16")
        string[] referenceParts = referenceText.Split(' ');
        string[] verseParts = referenceParts[1].Split(':');
        int startVerse = int.Parse(verseParts[1]);

        if (verseParts.Length > 2)
        {
            int endVerse = int.Parse(verseParts[2]);
            reference = new ScriptureReference(referenceParts[0], int.Parse(verseParts[0]), startVerse, endVerse);
        }
        else
        {
            reference = new ScriptureReference(referenceParts[0], int.Parse(verseParts[0]), startVerse, startVerse);
        }

        wordsLeft = words.Count;
    }

    private List<Word> GetNonHiddenWords()
    {
        return words.Where(word => !word.IsHidden()).ToList();
    }

    public bool HideWords(int count)
    {
        List<Word> nonHiddenWords = GetNonHiddenWords();
        if (nonHiddenWords.Count == 0)
        {
            return false; // All words are already hidden
        }

        Random rnd = new Random();
        for (int i = 0; i < count; i++)
        {
            int index = rnd.Next(nonHiddenWords.Count);
            nonHiddenWords[index].Hide();
            nonHiddenWords.RemoveAt(index); // Remove the hidden word from the list
            if (nonHiddenWords.Count == 0)
            {
                break;
            }
        }
        return true;
    }

    public void DisplayScripture()
    {
        Console.Clear();
        Console.WriteLine($"Reference: {reference.GetReference()}");
        foreach (Word word in words)
        {
            Console.Write($"{word.GetText()} ");
        }
        Console.WriteLine("\nPress Enter to continue or type 'quit' to exit:");
    }

    public bool IsFinished()
    {
        return wordsLeft <= 0;
    }

    public void UpdateWordsLeft()
    {
        wordsLeft = words.Count(word => !word.IsHidden());
    }
}

public class ScriptureMemorizer
{
    private List<Scripture> scriptures;
    private int currentScriptureIndex;
    private bool scriptureFinished;

    public ScriptureMemorizer()
    {
        scriptures = new List<Scripture>();

        // Add scriptures to the library
        AddScripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");
        AddScripture("Psalm 23:1", "The Lord is my shepherd; I shall not want.");
        // Add more scriptures as needed

        currentScriptureIndex = 0;
        scriptureFinished = false;
    }

    public void AddScripture(string reference, string text)
    {
        scriptures.Add(new Scripture(reference, text));
    }

    public void DisplayScripture()
    {
        if (currentScriptureIndex < scriptures.Count)
        {
            Scripture currentScripture = scriptures[currentScriptureIndex];
            currentScripture.DisplayScripture();

            if (!scriptureFinished)
            {
                if (!currentScripture.HideWords(3)) // Number of words to hide each time
                {
                    currentScripture.UpdateWordsLeft();
                    if (currentScripture.IsFinished())
                    {
                        Console.WriteLine("Type 'quite' to exit or any key to continue.");
                        scriptureFinished = true;
                    }
                }
            }
            else
            {
                string input = Console.ReadLine();
                if (input == "")
                {
                    currentScriptureIndex++;
                    scriptureFinished = false;
                    if (currentScriptureIndex >= scriptures.Count)
                    {
                        Console.WriteLine("All scriptures are finished. Press Enter to exit.");
                        return;
                    }
                    Console.WriteLine("\nNext Scripture:");
                }
            }
        }
        else
        {
            Console.WriteLine("All scriptures are finished. Type 'quit' to exit.");
        }
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        ScriptureMemorizer memorizer = new ScriptureMemorizer();

        while (true)
        {
            memorizer.DisplayScripture();

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
            {
                break;
            }
        }
    }
}


// I had my program work with a library of scriptures rather than a single one. I Choose scriptures at random to present to the user.