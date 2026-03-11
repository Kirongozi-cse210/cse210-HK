using System;
// This class handles the User Interface (the "Menu").
// It orchestrates the flow of the program by calling other classes.
class Program
{
    static void Main(string[] args)
    {
        // Instances of classes to abstract the logic away from Main.
        Journal journal = new Journal();
        PromptGenerator promptGen = new PromptGenerator();
         // AUTOMATIC LOAD: Checks for existing data upon startup.
        if (File.Exists("journal.csv"))
        { 
          journal.LoadFromCSV("journal.csv");

          Console.WriteLine("Previous journal entries loaded automatically.");
        }

        int choice = 0;
         // The Menu Loop for user interaction
        while (choice != 7)
        {
            Console.WriteLine("\nJournal Menu");
            Console.WriteLine("1. Write Entry");
            Console.WriteLine("2. Display Journal");
            Console.WriteLine("3. Save Journal");
            Console.WriteLine("4. Load Journal");
            Console.WriteLine("5. Search by Date");
            Console.WriteLine("6. Count Entries");
            Console.WriteLine("7. Exit");

            Console.Write("Choose option: ");
            choice = int.Parse(Console.ReadLine());

            if (choice == 1)
            {  // Abstraction: Program doesn't know how the prompt is picked.
                string prompt = promptGen.GetRandomPrompt();
                Console.WriteLine(prompt);

                Console.Write("Your response: ");
                string response = Console.ReadLine();

                Console.Write("Your mood today: ");
                string mood = Console.ReadLine();

                Entry entry = new Entry();

                
                // Encapsulation: Creating a single object to hold all entry data.
                DateTime now = DateTime.Now;
                entry._date = DateTime.Now.ToShortDateString();
                entry._time = now.ToShortTimeString(); // This adds the TIME (e.g., 2:30 PM)

                entry._promptText = prompt;
                entry._entryText = response;
                entry._mood = mood;
                // Add to list and immediately save to disk for data safety.
                journal.AddEntry(entry);

                journal.SaveToCSV("journal.csv");

                Console.WriteLine("Entry saved automatically to journal.csv!");
            }

            else if (choice == 2)
            {
                journal.DisplayAll();
            }

            else if (choice == 3)
            {
                Console.Write("Enter filename: ");
                string file = Console.ReadLine();

                journal.SaveToCSV(file);
            }

            else if (choice == 4)
            {
                Console.Write("Enter filename: ");
                string file = Console.ReadLine();

                journal.LoadFromCSV(file);
            }

            else if (choice == 5)
            {
                Console.Write("Enter date (MM/DD/YYYY): ");
                string date = Console.ReadLine();

                journal.SearchByDate(date);
            }

            else if (choice == 6)
            {
                journal.CountEntries();
            }
        }
    }
}
