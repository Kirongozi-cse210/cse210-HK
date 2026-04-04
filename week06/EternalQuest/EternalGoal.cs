using System;

public class EternalGoal : Goal
{
    private int _timesRecorded;

    public EternalGoal(string shortName, string description, string points) 
        : base(shortName, description, points) { }

    public override void RecordEvent()
    {
        _timesRecorded++;
    }

    public override bool IsComplete()
    {
        return false;  // Eternal goals are never complete
    }

    public override string GetDetailsString()
    {
        return $"{_shortName}: {_description}, Points: {_points}, Times Recorded: {_timesRecorded}";
    }

    public override string GetStringRepresentation()
    {
        return $"Eternal Goal: {_shortName}, {_points} points";
    }
}