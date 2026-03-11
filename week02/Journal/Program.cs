using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        Console.Write("Enter title: ");
        string title = Console.ReadLine();
        Console.Write("Enter content: ");
        string content = Console.ReadLine();
        Console.Write("Enter mood: ");
        string mood = Console.ReadLine();
        Console.Write("Is this an alert? (yes/no): ");
        bool alert = Console.ReadLine().ToLower() == "yes";

        JournalEntry entry = new JournalEntry(title, content, mood, alert);
        journal.AddEntry(entry);

        journal.DisplayEntries();

        journal.SaveToFile("journal.txt");

        Journal loadedJournal = new Journal();
        loadedJournal.LoadFromFile("journal.txt");
        loadedJournal.DisplayEntries();
    }
}