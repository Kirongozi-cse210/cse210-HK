/*
EXCEEDING REQUIREMENTS:

1. Activity Log:
   - The program tracks how many times each activity is completed.
   - Users can view their activity history from the menu.

2. Non-Repeating Prompts:
   - Prompts and questions are not repeated until all have been used.
   - This improves user experience and variety.

3. Enhanced Breathing Animation:
   - Instead of a simple countdown, a visual breathing animation was added.
   - This makes the breathing exercise more interactive and engaging.

4. Improved User Experience:
   - Cleaner structure using inheritance and reusable methods.
   - Better organization of code with encapsulation.
*/

class Program
{
    static int breathingCount = 0;
    static int reflectionCount = 0;
    static int listingCount = 0;

    static void Main(string[] args)
    {
        int choice = 0;

        while (choice != 5)
        {
            Console.Clear();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start breathing Activity");
            Console.WriteLine("2. Start reflection Activity");
            Console.WriteLine("3. Start listing Activity");
            Console.WriteLine("4. View Activity Log");
            Console.WriteLine("5. Quit");
            Console.Write("Select a choice from the menu: ");

            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    new BreathingActivity().Run();
                    breathingCount++;
                    break;

                case 2:
                    new ReflectionActivity().Run();
                    reflectionCount++;
                    break;

                case 3:
                    new ListingActivity().Run();
                    listingCount++;
                    break;

                case 4:
                    Console.WriteLine("\nActivity Log:");
                    Console.WriteLine($"Breathing: {breathingCount}");
                    Console.WriteLine($"Reflection: {reflectionCount}");
                    Console.WriteLine($"Listing: {listingCount}");
                    break;

                case 5:
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }
    }
}