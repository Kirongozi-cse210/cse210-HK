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
