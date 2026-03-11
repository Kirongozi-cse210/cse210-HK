using System;
using System.Collections.Generic;
using System.IO;

public class Journal {
    public List<JournalEntry> Entries { get; set; }

    public Journal() {
        Entries = new List<JournalEntry>();
    }

    public void AddEntry(JournalEntry entry) {
        Entries.Add(entry);
    }

    public void DisplayEntries() {
        foreach (var entry in Entries) {
            entry.Display();
            Console.WriteLine();
        }
    }

    public void SaveToFile(string filename) {
        using (StreamWriter outputFile = new StreamWriter(filename)) {
            foreach (var entry in Entries) {
                // Escape commas in the content
                string escapedContent = entry.Content.Replace(",", "<comma>");
                outputFile.WriteLine($"{entry.Title},{entry.Date.ToShortDateString()},{entry.Mood},{entry.Alert},{escapedContent}");
            }
        }
    }

    public void LoadFromFile(string filename) {
        try {
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines) {
                string[] parts = line.Split(",");
                if (parts.Length >= 5) {
                    string title = parts[0];
                    DateTime date = DateTime.Parse(parts[1]);
                    string mood = parts[2];
                    bool alert = bool.Parse(parts[3]);
                    // Unescape commas in the content
                    string content = string.Join(",", parts, 4, parts.Length - 4).Replace("<comma>", ",");

                    JournalEntry entry = new JournalEntry(title, content, mood, alert) {
                        Date = date
                    };
                    Entries.Add(entry);
                }
                else {
                    Console.WriteLine("Skipping an improperly formatted line.");
                }
            }
        } catch (FileNotFoundException) {
            Console.WriteLine("File not found. Please check the filename and try again.");
        } catch (Exception ex) {
            Console.WriteLine($"An error occurred while loading the file: {ex.Message}");
        }
    }
}