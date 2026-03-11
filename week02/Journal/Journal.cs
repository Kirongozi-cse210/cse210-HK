using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }

    public void DisplayAll()
    {
        foreach (Entry e in _entries)
        {
            e.Display();
        }
    }

    public void CountEntries()
    {
        Console.WriteLine($"Total Journal Entries: {_entries.Count}");
    }

    public void SearchByDate(string date)
    {
        foreach (Entry e in _entries)
        {
            if (e._date == date)
            {
                e.Display();
            }
        }
    }

    public void SaveToCSV(string filename)
    {
        using (StreamWriter output = new StreamWriter(filename))
        {
            foreach (Entry e in _entries)
            {
                output.WriteLine(e.ToCSV());
            }
        }
    }

    public void LoadFromCSV(string filename)
    {
        if (!File.Exists(filename))
        {
             
          Console.WriteLine("File not found. Please check the filename.");
          return;

        }
        string[] lines = File.ReadAllLines(filename);

        _entries.Clear();

        foreach (string line in lines)
        {
            string[] parts = line.Split(",");

            Entry entry = new Entry();
            entry._date = parts[0];
            entry._promptText = parts[1];
            entry._entryText = parts[2];
            entry._mood = parts[3];

            _entries.Add(entry);
        }
        
        Console.WriteLine("Journal loaded successfully!");
    }
}