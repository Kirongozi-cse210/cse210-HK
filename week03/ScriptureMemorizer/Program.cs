using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

/*
 * EXCEEDING REQUIREMENTS REPORT:
 * 1. Scripture Library: Loaded scriptures from a text file ('scriptures.txt') to allow a vast collection.
 * 2. Random Selection: The program picks a random verse from the loaded library for each session.
 * 3. First-Letter Hint Mode: To help with memorization blocks, the Word class was updated 
 *    to support showing just the first letter of hidden words as a mnemonic hint.
 */

class Program
{
    static void Main(string[] args)
    {
        string filename = "scriptures.txt";
        List<Scripture> library = LoadLibrary(filename);
        
        Random random = new Random();
        Scripture selectedScripture = library[random.Next(library.Count)];

        while (true)
        {
            Console.Clear();
            Console.WriteLine(selectedScripture.GetDisplayText());
            
            if (selectedScripture.IsCompletelyHidden())
            {
                Console.WriteLine("\nExcellent work! You've hidden all the words.");
                break;
            }

            Console.WriteLine("\nOptions: [Enter] Hide Words | [H] Hint Mode | type 'quit' to exit");
            string input = Console.ReadLine()?.ToLower();

            if (input == "quit") break;
            
            if (input == "h")
                selectedScripture.ToggleHintMode();
            else
                selectedScripture.HideRandomWords(3);
        }
    }

    static List<Scripture> LoadLibrary(string path)
    {
        var list = new List<Scripture>();
        if (!File.Exists(path))
        {
            File.WriteAllLines(path, new[] { "John 3:16|For God so loved the world that he gave his only begotten Son", "Proverbs 3:5-6|Trust in the Lord with all thine heart and lean not unto thine own understanding" });
        }

        foreach (string line in File.ReadAllLines(path))
        {
            string[] parts = line.Split('|');
            if (parts.Length == 2)
                list.Add(new Scripture(new Reference(parts[0]), parts[1]));
        }
        return list;
    }
}

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int? _endVerse;

    // Advanced constructor to parse strings like "John 3:16" or "Proverbs 3:5-6"
    public Reference(string referenceText)
    {
        string[] parts = referenceText.Split(' ');
        _book = parts[0];
        string[] numbers = parts[1].Split(':');
        _chapter = int.Parse(numbers[0]);
        
        if (numbers[1].Contains("-"))
        {
            string[] verses = numbers[1].Split('-');
            _verse = int.Parse(verses[0]);
            _endVerse = int.Parse(verses[1]);
        }
        else
        {
            _verse = int.Parse(numbers[1]);
            _endVerse = null;
        }
    }

    public string GetDisplayText() => _endVerse == null ? $"{_book} {_chapter}:{_verse}" : $"{_book} {_chapter}:{_verse}-{_endVerse}";
}

public class Word
{
    private string _text;
    private bool _isHidden;
    private bool _showHint;

    public Word(string text) { _text = text; _isHidden = false; }
    public void Hide() => _isHidden = true;
    public bool IsHidden() => _isHidden;
    public void SetHint(bool show) => _showHint = show;

    public string GetDisplayText()
    {
        if (!_isHidden) return _text;
        // Logic for "First Letter" hint mode
        if (_showHint && _text.Length > 0) return _text[0] + new string('_', _text.Length - 1);
        return new string('_', _text.Length);
    }
}

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private bool _hintMode = false;

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = text.Split(' ').Select(w => new Word(w)).ToList();
    }

    public void HideRandomWords(int numberToHide)
    {
        Random random = new Random();
        var visible = _words.Where(w => !w.IsHidden()).ToList();
        for (int i = 0; i < numberToHide && visible.Count > 0; i++)
        {
            int index = random.Next(visible.Count);
            visible[index].Hide();
            visible.RemoveAt(index);
        }
    }

    public void ToggleHintMode()
    {
        _hintMode = !_hintMode;
        foreach (var word in _words) word.SetHint(_hintMode);
    }

    public string GetDisplayText() => $"{_reference.GetDisplayText()}: {string.Join(" ", _words.Select(w => w.GetDisplayText()))}";
    public bool IsCompletelyHidden() => _words.All(w => w.IsHidden());
}