public class Fraction
{
    private int _top;
    private int _bottom;

    // Existing constructors...
    public Fraction() { _top = 1; _bottom = 1; }
    public Fraction(int top, int bottom) { _top = top; _bottom = bottom; }

    // New Constructor: Parses a string like "3/4"
    public Fraction(string fractionString)
    {
        string[] parts = fractionString.Split('/');

        if (parts.Length == 2)
        {
            _top = int.Parse(parts[0]);
            _bottom = int.Parse(parts[1]);
        }
        else
        {
            // If there's no "/", treat it as a whole number (e.g., "5")
            _top = int.Parse(parts[0]);
            _bottom = 1;
        }
    }

    public string GetFractionString() => $"{_top}/{_bottom}";
    public double GetDecimalValue() => (double)_top / _bottom;
}