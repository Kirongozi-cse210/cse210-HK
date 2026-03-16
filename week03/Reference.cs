public class Reference
{
    public string Book { get; set; }
    public int Chapter { get; set; }
    public int StartVerse { get; set; }
    public int? EndVerse { get; set; }

    public Reference(string book, int chapter, int startVerse, int? endVerse = null)
    {
        Book = book;
        Chapter = chapter;
        StartVerse = startVerse;
        EndVerse = endVerse;
    }

    public static Reference FromString(string referenceStr)
    {
        // Parse the reference string into book, chapter, startVerse, and endVerse
        var parts = referenceStr.Split(' ');
        var book = string.Join(" ", parts, 0, parts.Length - 1);
        var chapterVerse = parts[parts.Length - 1].Split(':');
        var chapter = int.Parse(chapterVerse[0]);
        var verses = chapterVerse[1].Split('-');
        var startVerse = int.Parse(verses[0]);
        int? endVerse = verses.Length > 1 ? int.Parse(verses[1]) : (int?)null;
        return new Reference(book, chapter, startVerse, endVerse);
    }

    public override string ToString()
    {
        if (EndVerse.HasValue)
            return $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
        else
            return $"{Book} {Chapter}:{StartVerse}";
    }
}