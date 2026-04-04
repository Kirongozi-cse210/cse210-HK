using System;

public class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string shortName, string description, string points) 
        : base(shortName, description, points) { }

    public override void RecordEvent()
    {
        _isComplete = true;
    }

    public override bool IsComplete()
    {
        return _isComplete;
    }

    public override string GetDetailsString()
    {
        return $"{_shortName}: {_description}, Points: {_points}";
    }

    public override string GetStringRepresentation()
    {
        return $"Simple Goal: {_shortName}, {_points} points";
    }
}