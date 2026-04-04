

using System;

class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();  // Initialize and load existing goals if any

        bool running = true;

        while (running)
        {
            // Display menu options
            Console.Clear();
            Console.WriteLine("===== Menu Options =====");
            Console.WriteLine("1. Create New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");
            Console.Write("Select a choice from the menu: ");

            string choice = Console.ReadLine();

            // Perform actions based on user choice
            switch (choice)
            {
                case "1":
                    manager.CreateGoal();  // Create new goal
                    break;

                case "2":
                    manager.ListGoals();  // List all goals
                    break;

                case "3":
                    manager.SaveGoals();  // Save goals to a file
                    break;

                case "4":
                    manager.LoadGoals();  // Load goals from a file
                    break;

                case "5":
                    manager.RecordEvent();  // Record an event for a goal
                    break;

                case "6":
                    running = false;  // Exit the loop and quit the program
                    break;

                default:
                    Console.WriteLine("Invalid option, try again.");
                    break;
            }

            // Wait for user input before continuing
            if (running)
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        Console.WriteLine("Thank you for using the Goal Tracker!");
    }
}