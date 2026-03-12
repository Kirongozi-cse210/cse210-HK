using System;
using System.Collections.Generic;
using System.IO;
// This class manages the collection of journal entries and file storage.
public class Journal
{// A private list to encapsulate the entries and protect the data.
    public List<Entry> _entries = new List<Entry>();
    // Adds a new entry object to the journal list.
    public void AddEntry(Entry entry)
    {
        _entries.Add(entry);
    }
    // Iterates through all entries and calls their internal display method.
    public void DisplayAll()
    {
        foreach (Entry e in _entries)
        {
            e.Display();
        }
    }
    // Provides a quick count of how many entries are in the system.
    public void CountEntries()
    {
        Console.WriteLine($"Total Journal Entries: {_entries.Count}");
    }
    // Searches the list for entries matching a specific date string.
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
    // Saves all current entries to a file using the | delimiter for data integrity.
    public void SaveToCSV(string filename)
    {
        using (StreamWriter output = new StreamWriter(filename))
        {
            foreach (Entry e in _entries)
            {   // Abstraction: Entry knows how to format itself for saving.
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
            string[] parts = line.Split("|"); // Use | for safety
            if (parts.Length >= 5) // Now we expect 5 parts
            {

               Entry entry = new Entry();
               entry._date = parts[0];
               entry._time = parts[1];        // NEW: Load the time
               entry._promptText = parts[2];
               entry._entryText = parts[3];
               entry._mood = parts[4];
               _entries.Add(entry);
            }
        }

        Console.WriteLine("Journal loaded successfully!");
    }
}