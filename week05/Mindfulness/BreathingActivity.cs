public class BreathingActivity : Activity
{
    public BreathingActivity() : base(
        "Breathing Activity",
        "Relax by following guided breathing."
    ) { }

    private void BreathingVisual(string text)
    {
        for (int i = 1; i <= 5; i++)
        {
            Console.Write($"\r{text} {new string('*', i)}");
            Thread.Sleep(400);
        }
        Console.WriteLine();
    }

    public void Run()
    {
        StartMessage();

        DateTime endTime = DateTime.Now.AddSeconds(_duration);

        while (DateTime.Now < endTime)
        {
            BreathingVisual("Breathe in...");
            BreathingVisual("Breathe out...");
        }

        EndMessage();
    }
}