using System;
using System.Collections.Generic;
using System.IO;

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
                selectedScripture.HideRandomWords(3, random);
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
