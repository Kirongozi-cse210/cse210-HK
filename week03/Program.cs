using System;
using System.Collections.Generic;
using System.IO;

/*
 * EXCEEDING REQUIREMENTS REPORT:
 * 
 * This program goes beyond the core requirements in the following ways:
 * 
 * 1. Scripture Library from File:
 *    Instead of hardcoding a single scripture, the program loads multiple scriptures
 *    from an external file (scriptures.txt). This allows easy expansion and customization.
 * 
 * 2. Random Scripture Selection:
 *    Each time the program runs, a random scripture is selected from the library,
 *    providing a different memorization experience.
 * 
 * 3. Hint Mode (First-Letter Support):
 *    A hint mode can be toggled by the user. When enabled, hidden words display only
 *    their first letter followed by underscores, helping users recall words without
 *    revealing them completely.
 */
class Program
{
    static void Main(string[] args)
    {
        string filename = "scriptures.txt";
        List<Scripture> library = LoadLibrary(filename);

        if (library.Count == 0)
        {
            Console.WriteLine("No scriptures found.");
            return;
        }

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

            if (input == "quit")
                break;

            if (input == "h")
                selectedScripture.ToggleHintMode();
            else
                selectedScripture.HideRandomWords(3);
        }
    }

    static List<Scripture> LoadLibrary(string path)
    {
        List<Scripture> list = new List<Scripture>();

        if (!File.Exists(path))
        {
            File.WriteAllLines(path, new[]
            {
                "John 3:16|For God so loved the world that he gave his only begotten Son",
                "Proverbs 3:5-6|Trust in the Lord with all thine heart and lean not unto thine own understanding"
            });
        }

        foreach (string line in File.ReadAllLines(path))
        {
            string[] parts = line.Split('|');

            if (parts.Length == 2)
            {
                list.Add(new Scripture(new Reference(parts[0]), parts[1]));
            }
        }

        return list;
    }
}
