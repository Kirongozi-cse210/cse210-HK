using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;
    private string _filePath = "goals.txt";

    public GoalManager() { }

    // Initialize and load goals
    public void Start()
    {
        LoadGoals();  // Load existing goals from file if any
    }

    // Display player's current score
    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"Player's Score: {_score}");
        Console.WriteLine("Goals Created: ");
        ListGoals();
    }

    // List all created goals
    public void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals available.");
            return;
        }

        foreach (var goal in _goals)
        {
            Console.WriteLine(goal.GetStringRepresentation());
        }
    }

    // Create a new goal based on user input
    public void CreateGoal()
    {
        Console.WriteLine("Choose a goal type:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        string choice = Console.ReadLine();

        Console.Write("Enter goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter goal description: ");
        string description = Console.ReadLine();
        Console.Write("Enter points for this goal: ");
        string points = Console.ReadLine();

        Goal newGoal = null;

        // Depending on user choice, create the corresponding goal
        switch (choice)
        {
            case "1":
                newGoal = new SimpleGoal(name, description, points);
                break;
            case "2":
                newGoal = new EternalGoal(name, description, points);
                break;
            case "3":
                Console.Write("How many times must this goal be completed to finish? ");
                int target = int.Parse(Console.ReadLine());
                Console.Write("Enter bonus points for completing the goal: ");
                int bonus = int.Parse(Console.ReadLine());
                newGoal = new ChecklistGoal(name, description, points, target, bonus);
                break;
            default:
                Console.WriteLine("Invalid choice.");
                return;
        }

        _goals.Add(newGoal);
        Console.WriteLine($"Goal '{name}' created successfully!");
    }

    // Save all goals to a file
    public void SaveGoals()
    {
        using (StreamWriter writer = new StreamWriter(_filePath))
        {
            foreach (var goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());  // Save goal data to file
            }
        }
        Console.WriteLine("Goals saved successfully!");
    }

    // Load goals from a file
    public void LoadGoals()
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("No goals file found.");
            return;
        }

        var lines = File.ReadAllLines(_filePath);
        foreach (var line in lines)
        {
            var parts = line.Split(',');

            Goal goal = null;

            if (parts[0] == "Simple")
            {
                if (parts.Length != 4) continue; // Ensure correct format for Simple Goal
                goal = new SimpleGoal(parts[1], parts[2], parts[3]);
            }
            else if (parts[0] == "Eternal")
            {
                if (parts.Length != 4) continue; // Ensure correct format for Eternal Goal
                goal = new EternalGoal(parts[1], parts[2], parts[3]);
            }
            else if (parts[0] == "Checklist")
            {
                if (parts.Length != 6) continue; // Checklist Goal requires exactly 6 parts
                goal = new ChecklistGoal(parts[1], parts[2], parts[3], int.Parse(parts[4]), int.Parse(parts[5]));
            }

            if (goal != null)
            {
                _goals.Add(goal);  // Add valid goals to the list
            }
        }

        Console.WriteLine("Goals loaded successfully!");
    }
}