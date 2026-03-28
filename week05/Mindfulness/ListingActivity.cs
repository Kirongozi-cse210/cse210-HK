public class ListingActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Who are people you appreciate?",
        "What are your strengths?",
        "Who have you helped recently?",
        "Who are your heroes?"
    };

    private List<string> _unusedPrompts;

    public ListingActivity() : base(
        "Listing Activity",
        "List positive things in your life."
    )
    {
        _unusedPrompts = new List<string>(_prompts);
    }

    private string GetPrompt()
    {
        if (_unusedPrompts.Count == 0)
            _unusedPrompts = new List<string>(_prompts);

        Random rand = new Random();
        int index = rand.Next(_unusedPrompts.Count);
        string prompt = _unusedPrompts[index];
        _unusedPrompts.RemoveAt(index);

        return prompt;
    }

    public void Run()
    {
        StartMessage();

        Console.WriteLine($"\n{GetPrompt()}");
        Console.WriteLine("Start in...");
        ShowCountdown(5);

        int count = 0;
        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.Write("> ");
            Console.ReadLine();
            count++;
        }

        Console.WriteLine($"\nYou listed {count} items!");
        EndMessage();
    }
}