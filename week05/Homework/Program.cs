using System;

class Program
{
    static void Main(string[] args)
    {
        Assignment assignment = new MathAssignment("Roberto Rodriguez", "Fractions", "7.3", "8-19");
        Console.WriteLine(assignment.GetSummary());
        Console.WriteLine(((MathAssignment)assignment).GetHomeworkList());

        assignment = new WritingAssignment("Mary Waters", "European History", "The Causes of World War II");
        Console.WriteLine(assignment.GetSummary());
        Console.WriteLine(((WritingAssignment)assignment).GetWritingInformation());
    }
}
