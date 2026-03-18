using System.Linq;

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int? _endVerse;

    public Reference(string referenceText)
    {
        string[] parts = referenceText.Split(' ');

        _book = string.Join(" ", parts.Take(parts.Length - 1));
        string[] numbers = parts.Last().Split(':');

        _chapter = int.Parse(numbers[0]);

        if (numbers[1].Contains("-"))
        {
            string[] verses = numbers[1].Split('-');
            _verse = int.Parse(verses[0]);
            _endVerse = int.Parse(verses[1]);
        }
        else
        {
            _verse = int.Parse(numbers[1]);
            _endVerse = null;
        }
    }

    public string GetDisplayText()
    {
        if (_endVerse == null)
            return $"{_book} {_chapter}:{_verse}";
        else
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
    }
}