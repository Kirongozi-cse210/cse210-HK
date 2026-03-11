public class JournalEntry
{
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    public string Mood { get; set; }
    public bool Alert { get; set; }

    public JournalEntry(string title, string content, string mood, bool alert)
    {
        Title = title;
        Content = content;
        Date = DateTime.Now;
        Mood = mood;
        Alert = alert;
    }

    public void Display()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Date: {Date.ToShortDateString()}");
        Console.WriteLine($"Mood: {Mood}");
        Console.WriteLine($"Alert: {(Alert ? "Yes" : "No")}");
        Console.WriteLine($"Content: {Content}");
    }
}
Benjamin
using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<JournalEntry> Entries { get; set; }

    public Journal()
    {
        Entries = new List<JournalEntry>();
    }

    public void AddEntry(JournalEntry entry)
    {
        Entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in Entries)
        {
            entry.Display();
            Console.WriteLine();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter outputFile = new StreamWriter(filename))
        {
            foreach (var entry in Entries)
            {
                outputFile.WriteLine($"{entry.Title},{entry.Date.ToShortDateString()},{entry.Mood},{entry.Alert},{entry.Content}");
            }
        }
    }

    public void LoadFromFile(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        foreach (string line in lines)
        {
            string[] parts = line.Split(",");
            string title = parts[0];
            DateTime date = DateTime.Parse(parts[1]);
            string mood = parts[2];
            bool alert = bool.Parse(parts[3]);
            string content = parts[4];

            JournalEntry entry = new JournalEntry(title, content, mood, alert);
            entry.Date = date;
            Entries.Add(entry);
        }
    }
}