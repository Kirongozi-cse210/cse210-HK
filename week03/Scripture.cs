using System;
using System.Collections.Generic;
using System.Linq;

public class Scripture
{
    public Reference Reference { get; set; }
    public List<Word> Words { get; set; }

    public Scripture(Reference reference, string text)
    {
        Reference = reference;
        Words = text.Split(new[] { ' ' }, StringSplitOptions.None)
            .Select(word => new Word(word))
            .ToList();
    }

    public void Display()
    {
        Console.WriteLine(Reference.ToString());
        Console.WriteLine(string.Join(" ", Words.Select(w => w.ToString())));
    }

    public void HideRandomWord()
    {
        var visibleWords = Words.Where(w => !w.Hidden).ToList();
        if (visibleWords.Any())
        {
            var random = new Random();
            var wordToHide = visibleWords[random.Next(visibleWords.Count)];
            wordToHide.Hide();
        }
    }

    public bool AllWordsHidden()
    {
        return Words.All(w => w.Hidden);
    }
}