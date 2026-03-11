using System;
using System.Collections.Generic;
// This class abstracts the logic for selecting a random prompt.
// It hides the list of prompts from the rest of the program.
public class PromptGenerator
{
    // A private list to encapsulate the prompts
    public List<string> _prompts = new List<string>()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "What challenge did I face today?",
        "What made me smile today?",
        "What did I learn today?",
        "What am I grateful for today?",
        "What was the most productive thing I did today?"
    };
     // This method provides a random prompt without the main program 
    // needing to know how the selection is made.
    public string GetRandomPrompt()
    {   // Use the Random class to pick a unique index.
        Random rand = new Random();
        int index = rand.Next(_prompts.Count);
        // Return the selected prompt.
        return _prompts[index];
    }
}