namespace PangyaAPI.Json.Flags
{
    /// <summary>
    /// Possible JSON tokens in parsed input.
    /// </summary>
    public enum JsonToken
    {
        Unknown,
        LeftBrace,
        RightBrace,
        Colon,
        Comma,
        LeftBracket,
        RightBracket,
        String,
        Number,
        True,
        False,
        Null
    }

}
