using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        // 1. Initialize objects
        var reference = Reference.FromString("John 3:16");
        var scripture = new Scripture(reference, "For God so loved the world that he gave his one and only Son");

        while (true)
        {
            Console.Clear();
            scripture.Display();
            
            // 2. Check if all words are hidden BEFORE prompting
            if (scripture.AllWordsHidden())
            {
                Console.WriteLine("\nAll words hidden. Exiting...");
                break;
            }

            Console.WriteLine("\nPress Enter to hide words, type 'quit' to exit.");
            var input = Console.ReadLine();
            
            if (input?.ToLower() == "quit")
                break;

            scripture.HideRandomWords(3); // Hides 3 random words at a time
        }
    }
}

// Represents the Bible reference (e.g., John 3:16)
public class Reference
{
    private string _text;
    private Reference(string text) => _text = text;
    public static Reference FromString(string text) => new Reference(text);
    public override string ToString() => _text;
}

// Represents a single word and its visibility
public class Word
{
    private string _text;
    private bool _isHidden;

    public Word(string text) => _text = text;
    public void Hide() => _isHidden = true;
    public bool IsHidden => _isHidden;
    public string GetDisplayText() => _isHidden ? new string('_', _text.Length) : _text;
}

// Manages the collection of words and the reference
public class Scripture
{
    private Reference _reference;
    private List<Word> _words;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void Display()
    {
        Console.WriteLine($"{_reference}: {string.Join(" ", _words.Select(w => w.GetDisplayText()))}");
    }

    public void HideRandomWords(int count)
    {
        Random random = new Random();
        var visibleWords = _words.Where(w => !w.IsHidden).ToList();
        
        for (int i = 0; i < count && visibleWords.Any(); i++)
        {
            int index = random.Next(visibleWords.Count);
            visibleWords[index].Hide();
            visibleWords.RemoveAt(index);
        }
    }

    public bool AllWordsHidden() => _words.All(w => w.IsHidden);
}