public class ReflectionActivity : Activity
{
    private List<string> _prompts = new List<string>
    {
        "Think of a time when you stood up for someone.",
        "Think of a time you did something difficult.",
        "Think of a time you helped someone.",
        "Think of a time you were selfless."
    };

    private List<string> _questions = new List<string>
    {
        "Why was this meaningful?",
        "How did you feel?",
        "What did you learn?",
        "How can you use this again?",
        "What made it special?"
    };

    private List<string> _unusedPrompts;
    private List<string> _unusedQuestions;

    public ReflectionActivity() : base(
        "Reflection Activity",
        "Reflect on your strengths."
    )
    {
        _unusedPrompts = new List<string>(_prompts);
        _unusedQuestions = new List<string>(_questions);
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

    private string GetQuestion()
    {
        if (_unusedQuestions.Count == 0)
            _unusedQuestions = new List<string>(_questions);

        Random rand = new Random();
        int index = rand.Next(_unusedQuestions.Count);
        string question = _unusedQuestions[index];
        _unusedQuestions.RemoveAt(index);

        return question;
    }

    public void Run()
    {
        StartMessage();

        Console.WriteLine($"\n{GetPrompt()}");
        Console.WriteLine("Think about it...");
        ShowSpinner(5);

        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            Console.WriteLine($"\n{GetQuestion()}");
            ShowSpinner(4);
        }

        EndMessage();
    }
}