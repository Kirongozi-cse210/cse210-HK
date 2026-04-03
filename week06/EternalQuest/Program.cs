// EXCEEDING REQUIREMENTS:
// - Fixed loading system using '|' separator (handles commas safely)
// - Added date/time tracking (created + last completed)
// - Implemented safe input handling (TryParse)
// - Added level system based on score
// - Clean architecture using GoalManager class

using System;

class Program
{
    static void Main(string[] args)
    {
        new GoalManager().Start();
    }
}