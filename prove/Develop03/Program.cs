using System;
using System.Collections.Generic;
using System.Linq;

// Class to represent a single word in the scripture text
public class ScriptureWord
{
    public string Word { get; private set; }
    public bool Hidden { get; set; }

    public ScriptureWord(string word)
    {
        Word = word;
        Hidden = false;
    }

    // Override ToString to show the word or a placeholder if hidden
    public override string ToString()
    {
        return Hidden ? "___" : Word;
    }
}

// Class to represent a scripture reference (e.g., "John 3:16")
public class ScriptureReference
{
    public string Book { get; private set; }
    public int Chapter { get; private set; }
    public int VerseStart { get; private set; }
    public int? VerseEnd { get; private set; }

    public ScriptureReference(string reference)
    {
        // Parse the scripture reference
        // Example supported formats: "John 3:16", "Proverbs 3:5-6"
        string[] parts = reference.Split(':');
        Book = parts[0];
        string versePart = parts[1];

        string[] verseParts = versePart.Split('-');
        Chapter = int.Parse(verseParts[0]);

        if (verseParts.Length > 1)
        {
            string[] endVerseParts = verseParts[1].Split('-');
            VerseStart = int.Parse(endVerseParts[0]);
            VerseEnd = int.Parse(endVerseParts[1]);
        }
        else
        {
            VerseStart = int.Parse(verseParts[0]);
            VerseEnd = null;
        }
    }
}

// Class to represent a scripture with reference and text
public class Scripture
{
    public ScriptureReference Reference { get; private set; }
    public List<ScriptureWord> Words { get; private set; }

    public Scripture(string reference, string text)
    {
        Reference = new ScriptureReference(reference);
        Words = text.Split(' ').Select(word => new ScriptureWord(word)).ToList();
    }

    // Display the scripture with its reference
    public void Display()
    {
        Console.WriteLine($"{Reference.Book} {Reference.Chapter}:{Reference.VerseStart}-{Reference.VerseEnd}");
        foreach (var word in Words)
        {
            Console.Write(word + " ");
        }
        Console.WriteLine("\n");
    }

    // Hide a random word in the scripture
    public bool HideRandomWord()
    {
        var visibleWords = Words.Where(word => !word.Hidden).ToList();
        if (visibleWords.Count == 0)
        {
            return false; // No more words to hide
        }

        Random rand = new Random();
        int indexToHide = rand.Next(0, visibleWords.Count);
        visibleWords[indexToHide].Hidden = true;
        return true;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        // Create a scripture object
        string scriptureRef = "John 3:16";
        string scriptureText = "For God so loved the world, that he gave his only Son, that whoever believes in him should not perish but have eternal life.";
        Scripture scripture = new Scripture(scriptureRef, scriptureText);

        bool continueHiding = true;

        while (continueHiding)
        {
            Console.Clear();
            scripture.Display();

            Console.WriteLine("Press Enter to hide another word or type 'quit' to exit.");
            string input = Console.ReadLine().Trim().ToLower();

            if (input == "quit")
            {
                continueHiding = false;
            }
            else
            {
                continueHiding = scripture.HideRandomWord();
            }
        }

        Console.WriteLine("All words in the scripture are hidden. Program ended.");
    }
}
