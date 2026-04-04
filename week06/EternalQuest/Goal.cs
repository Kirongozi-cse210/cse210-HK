using System;
using System.Collections.Generic;
using System.IO;

/*
===========================================
        ETERNAL QUEST PROGRAM
===========================================

This program is a goal tracking system with
gamification features to motivate users.

CORE FEATURES:
- Create and manage goals (Simple, Eternal, Checklist)
- Earn points by completing goals
- Save and load progress

EXTRA GAMIFICATION FEATURES:

⭐ LEVEL SYSTEM
- Player levels increase every 1000 points
- Level = Score / 1000
- Displays current level to motivate progress

🔥 STREAK TRACKING
- Tracks how many times goals are completed in a row
- Each recorded event increases streak
- Streak resets only if program restarts (simple version)

🎮 BONUS SYSTEM
- Every 5 streak → +100 bonus points
- Checklist completion → bonus reward
- Encourages consistency and repetition

===========================================
*/

//////////////////////////////////////////////////
// BASE CLASS
//////////////////////////////////////////////////
public abstract class Goal
{
    public string _shortName;    
    public string _description;  
    public int _points;          
    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    public abstract int RecordEvent();
    public abstract bool IsComplete();

    
    public virtual string GetDetailsString()
    {
        string checkbox = IsComplete() ? "[X]" : "[ ]";
        return $"{checkbox} {_shortName} ({_description})";
    }

    public abstract string GetStringRepresentation();
}