namespace Nom;

public static class NomStringExtensions
{
    public static StringParsable AsParsable(this string input)
    {
        return new StringParsable(input);
    }
}
