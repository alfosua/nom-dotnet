using Nom.Sdk;

namespace Nom.Characters;

public interface ILineEndingParser<T> : IParser<T, T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
}

public class LineEndingParser<T> : ILineEndingParser<T>
    where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
{
    public IResult<T, T> Parse(T input)
    {
        CommonParsings.CheckInputIsNotEmpty(input);

        var positionToSplit = 0;
        var carriagePresentBefore = false;
        foreach (var ch in input.EnumerateContent())
        {
            if (ch == '\r')
            {
                if (carriagePresentBefore)
                {
                    break;
                }

                carriagePresentBefore = true;
            }
            if (ch == '\n')
            {
                positionToSplit = carriagePresentBefore ? 2 : 1;
                break;
            }
            else
            {
                break;
            }
        }

        if (positionToSplit == 0)
        {
            throw new Exception("Could not parse a line ending at head");
        }

        var (output, remainder) = input.SplitAtPosition(positionToSplit);

        return Result.Create(remainder, output);
    }
}

public static class LineEnding
{
    public static IParser<StringParsable, StringParsable> Create() => new LineEndingParser<StringParsable>();

    public static IParser<T, T> Create<T>()
        where T : IParsable, IContentEnumerable<char>, ISplitableAtPosition<T>, IEmptyCheckable
        => new LineEndingParser<T>();
}
