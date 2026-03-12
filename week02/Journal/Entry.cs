using System;

public class Entry
{
    public string _date;
    public string _time; // ADD THIS LINE
    public string _promptText;
    public string _entryText;
    public string _mood;

    public void Display()
    {
        // Added _time to the display output
        Console.WriteLine($"Date: {_date} Time: {_time} - Prompt: {_promptText}");
        Console.WriteLine($"Prompt: {_promptText}");
        Console.WriteLine($"- {_entryText}");
        Console.WriteLine($"Mood: {_mood}");
        Console.WriteLine("--------------------------------");
    }

    public string ToCSV()
    {
        // Add _time to the save string (using | to avoid comma errors)
        return $"{_date}|{_time}|{_promptText}|{_entryText}|{_mood}";
    }
}