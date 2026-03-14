using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Journal
{
    private List<Entry> _entries = new List<Entry>();

    public void AddEntry(Entry newEntry) => _entries.Add(newEntry);

    public void DisplayAll()
    {
        foreach (var e in _entries) e.Display();
    }

    public void DisplayAllWithIndexes()
    {
        for (int i = 0; i < _entries.Count; i++)
        {
            Console.Write($"[{i}] ");
            _entries[i].Display();
        }
    }

    public void EditEntry(int index)
    {
        if (index >= 0 && index < _entries.Count)
        {
            Console.Write("Enter new response: ");
            _entries[index]._entryText = Console.ReadLine();
            Console.Write("Update mood: ");
            _entries[index]._mood = Console.ReadLine();
            Console.WriteLine("Entry updated!");
        }
    }

    public void SaveToCSV(string filename)
    {
        using (StreamWriter sw = new StreamWriter(filename))
        {
            foreach (var e in _entries)
                sw.WriteLine($"{e._date}|{e._time}|{e._promptText}|{e._entryText}|{e._mood}");
        }
    }

    public void LoadFromCSV(string filename)
    {
        if (File.Exists(filename))
        {
            _entries.Clear(); // Clears memory to load the new file
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] p = line.Split('|');
                if (p.Length == 5)
                {
                    _entries.Add(new Entry { _date=p[0], _time=p[1], _promptText=p[2], _entryText=p[3], _mood=p[4] });
                }
            }
        }
    }

    public void SearchByDate(string date)
    {
        var found = _entries.Where(e => e._date == date).ToList();
        if (found.Any()) foreach (var f in found) f.Display();
        else Console.WriteLine("No entries found for that date.");
    }

    public void CountEntries() => Console.WriteLine($"Total Entries: {_entries.Count}");
}