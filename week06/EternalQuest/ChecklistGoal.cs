using System;

public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    public ChecklistGoal(string shortName, string description, string points, int target, int bonus) 
        : base(shortName, description, points)
    {
        _target = target;
        _bonus = bonus;
    }

    public override void RecordEvent()
    {
        _amountCompleted++;
        if (_amountCompleted >= _target)
        {
            Console.WriteLine($"Bonus awarded: {_bonus} points!");
        }
    }

    public override bool IsComplete()
    {
        return _amountCompleted >= _target;
    }

    public override string GetDetailsString()
    {
        return $"{_shortName}: {_description}, Target: {_target}, Completed: {_amountCompleted}, Bonus: {_bonus}";
    }

    public override string GetStringRepresentation()
    {
        return $"Checklist Goal: {_shortName}, {_points} points, Target: {_target}, Completed: {_amountCompleted}";
    }
}