
using System;


public class GoalManager
{
    private List<Goal> _goals;
    private int _score;
    private int _streak;

    public GoalManager()
    {
        _goals = new List<Goal>();
        _score = 0;
        _streak = 0;
    }

    public void Start()
    {
        int choice = 0;

        while (choice != 7)
        {
            DisplayPlayerInfo();

            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goal Names");
            Console.WriteLine("3. List Goal Details");
            Console.WriteLine("4. Record Event");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Load Goals");
            Console.WriteLine("7. Quit");

            Console.Write("Select a choice from the menu: ");
            int.TryParse(Console.ReadLine(), out choice);

            switch (choice)
            {
                case 1: CreateGoal(); break;
                case 2: ListGoalNames(); break;
                case 3: ListGoalDetails(); break;
                case 4: RecordEvent(); break;
                case 5: SaveGoals(); break;
                case 6: LoadGoals(); break;
                case 7: return;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        int level = _score / 1000;

        Console.WriteLine($"\n>> Score: {_score}");
        Console.WriteLine($">> Level: {level}");
        Console.WriteLine($">> Streak: {_streak}\n");
    }

    public void ListGoalNames()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i]._shortName}");
        }
    }

    public void ListGoalDetails()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }
    public void CreateGoal()
    {
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        Console.Write("Which type of goal would you like to create? >: ");
        int type = int.Parse(Console.ReadLine());

        Console.Write("What is the name of your goal? >: ");
        string name = Console.ReadLine();

        Console.Write("What is a short description of it? >: ");
        string desc = Console.ReadLine();

        Console.Write("What is the amount of points associated with this goal? >: ");
        int points = int.Parse(Console.ReadLine());

        if (type == 1)
            _goals.Add(new SimpleGoal(name, desc, points));

        else if (type == 2)
            _goals.Add(new EternalGoal(name, desc, points));

        else if (type == 3)
        {
            Console.Write("What is the target for accomplishing it that many times? >: ");
            int target = int.Parse(Console.ReadLine());

            Console.Write("How many times does this goal need to be accomplished for a bonus? >: ");
            int bonus = int.Parse(Console.ReadLine());

            _goals.Add(new ChecklistGoal(name, desc, points, target, bonus));
        }
    }

    public void RecordEvent()
    {
        ListGoalNames();
        Console.Write("Select goal: ");
        int index = int.Parse(Console.ReadLine()) - 1;

        int points = _goals[index].RecordEvent();

        if (points > 0)
        {
            _streak++;

            // 🎮 STREAK BONUS
            if (_streak % 5 == 0)
            {
                Console.WriteLine(">>STREAK BONUS! +100 points!");
                points += 100;
            }
        }

        _score += points;

        Console.WriteLine($"You earned {points} points!");
    }

    public void SaveGoals()
    {
        using (StreamWriter sw = new StreamWriter("goals.txt"))
        {
            sw.WriteLine(_score);
            sw.WriteLine(_streak);

            foreach (Goal g in _goals)
            {
                sw.WriteLine(g.GetStringRepresentation());
            }
        }
    }

    public void LoadGoals()
    {
        if (!File.Exists("goals.txt")) return;

        string[] lines = File.ReadAllLines("goals.txt");

        _goals.Clear();
        _score = int.Parse(lines[0]);
        _streak = int.Parse(lines[1]);

        for (int i = 2; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split("|");

            if (parts[0] == "SimpleGoal")
                _goals.Add(new SimpleGoal(parts[1], parts[2], int.Parse(parts[3]), bool.Parse(parts[4])));

            else if (parts[0] == "EternalGoal")
                _goals.Add(new EternalGoal(parts[1], parts[2], int.Parse(parts[3])));

            else if (parts[0] == "ChecklistGoal")
                _goals.Add(new ChecklistGoal(parts[1], parts[2], int.Parse(parts[3]),
                    int.Parse(parts[4]), int.Parse(parts[5]), int.Parse(parts[6])));
        }
    }
}