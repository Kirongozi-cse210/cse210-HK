using System;
using System.Collections.Generic;
using System.IO;

public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public GoalManager()
    {
        // Automatically load saved goals if the file exists
        if (File.Exists("goals.txt"))
        {
            Load();
        }
    }

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("\n=== Eternal Quest ===");
            Console.WriteLine($"Score: {_score} | Level: {_score / 1000}");

            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Record Event");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Load");
            Console.WriteLine("6. Quit");

            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            if (choice == "1") CreateGoal();
            else if (choice == "2") ListGoals();
            else if (choice == "3") RecordEvent();
            else if (choice == "4") Save();
            else if (choice == "5") Load();
            else if (choice == "6") break;
        }
    }

    private void ListGoals()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals yet.");
            return;
        }

        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    private void CreateGoal()
    {
        Console.WriteLine("1. Simple  2. Eternal  3. Checklist");
        string type = Console.ReadLine();

        Console.Write("Name: ");
        string name = Console.ReadLine();

        Console.Write("Description: ");
        string desc = Console.ReadLine();

        int points = GetInt("Points: ");

        if (type == "1")
            _goals.Add(new SimpleGoal(name, desc, points));

        else if (type == "2")
            _goals.Add(new EternalGoal(name, desc, points));

        else if (type == "3")
        {
            int target = GetInt("Target: ");
            int bonus = GetInt("Bonus: ");
            _goals.Add(new ChecklistGoal(name, desc, points, target, 0, bonus));
        }

        Console.WriteLine("Goal created!");
    }

    private void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals to record yet.");
            return;
        }

        ListGoals();
        int index = GetInt("Select goal: ", 1, _goals.Count) - 1;

        int earned = _goals[index].RecordEvent();
        _score += earned;

        Console.WriteLine($"You earned {earned} points!");
    }

    private void Save()
    {
        using (StreamWriter writer = new StreamWriter("goals.txt"))
        {
            writer.WriteLine(_score);
            foreach (Goal g in _goals)
                writer.WriteLine(g.GetStringRepresentation());
        }
        Console.WriteLine("Saved!");
    }

    private void Load()
    {
        if (!File.Exists("goals.txt"))
        {
            Console.WriteLine("No file found.");
            return;
        }

        string[] lines = File.ReadAllLines("goals.txt");
        _goals.Clear();
        _score = int.Parse(lines[0]);

        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(":");
            string type = parts[0];
            string[] d = parts[1].Split("|");

            if (type == "SimpleGoal")
                _goals.Add(new SimpleGoal(d[0], d[1], int.Parse(d[2]), bool.Parse(d[3])));
            else if (type == "EternalGoal")
                _goals.Add(new EternalGoal(d[0], d[1], int.Parse(d[2])));
            else if (type == "ChecklistGoal")
                _goals.Add(new ChecklistGoal(d[0], d[1], int.Parse(d[2]),
                    int.Parse(d[3]), int.Parse(d[4]), int.Parse(d[5])));
        }

        Console.WriteLine("Goals loaded successfully!");
    }

    private int GetInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
    {
        int value;
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                return value;

            Console.WriteLine("Invalid number.");
        }
    }
}