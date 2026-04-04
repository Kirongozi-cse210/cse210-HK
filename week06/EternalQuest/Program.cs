/*
===========================================================
        EXCEEDING REQUIREMENTS REPORT
===========================================================

In addition to completing all core requirements for the 
Eternal Quest program, the following extra features were 
implemented to enhance functionality and user engagement 
through gamification:

⭐ LEVEL SYSTEM
- The program now includes a leveling system based on the 
  user's total score.
- The level is calculated as: Level = Score / 1000.
- This provides long-term motivation by rewarding users 
  as they accumulate points over time.
- The current level is displayed alongside the score.

🔥 STREAK TRACKING
- A streak system has been added to track consecutive goal 
  completions.
- Each time the user records a goal event, the streak increases.
- This encourages consistency and regular participation.
- The current streak is displayed to the user.

🎮 GAMIFICATION BONUS SYSTEM
- Additional bonus points are awarded for maintaining streaks.
- Every 5 consecutive goal completions grants an extra 
  +100 bonus points.
- Checklist goals already include a completion bonus, and this 
  system enhances that further by rewarding consistent behavior.
- Bonus messages are displayed to reinforce positive feedback.

💾 ENHANCED SAVE/LOAD SYSTEM
- The program now saves and loads not only goals and score, 
  but also the user's streak.
- This ensures that all progress is preserved between sessions.

🎯 OVERALL IMPROVEMENT
These enhancements transform the program from a basic goal 
tracker into a more engaging and motivating system by:
- Encouraging consistency (streaks)
- Rewarding long-term progress (levels)
- Providing immediate incentives (bonuses)

===========================================================
*/
class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}