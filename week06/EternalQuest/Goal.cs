using System;

public abstract class Goal
{
    protected string _shortName;
    protected string _description;
    protected string _points;

    public Goal(string shortName, string description, string points)
    {
        _shortName = shortName;
        _description = description;
        _points = points;
    }

    // Abstract methods to be implemented by derived classes
    public abstract void RecordEvent();  // Method to be implemented by derived classes
    public abstract bool IsComplete();  // Check if the goal is completed
    public abstract string GetDetailsString();  // Get detailed string about the goal
    public abstract string GetStringRepresentation();  // Get short representation for listing
}