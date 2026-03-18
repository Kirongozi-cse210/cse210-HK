public class Word
{
    private string _text;
    private bool _isHidden;
    private bool _showHint;

    public Word(string text)
    {
        _text = text;
        _isHidden = false;
        _showHint = false;
    }

    public void Hide()
    {
        _isHidden = true;
    }

    public bool IsHidden()
    {
        return _isHidden;
    }

    public void SetHint(bool show)
    {
        _showHint = show;
    }

    public string GetDisplayText()
    {
        if (!_isHidden)
            return _text;

        if (_showHint && _text.Length > 0)
            return _text[0] + new string('_', _text.Length - 1);

        return new string('_', _text.Length);
    }
}