using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Nom;

public record StringParsable : IParsable
    , IWithContent<ReadOnlyMemory<char>>
    , ISplitableAtPosition<StringParsable>
    , IRemainder
    , IEmptyCheckable
    , IRegexMatchable
    , IContentVisitable<string>
    , IContentVisitable<char>
    , IEmptyTailParsableFactory<StringParsable>
    , IEmptyTailParsableDecorator
    , IContentMeasurable
    , IContentEnumerable<char>
{
    public static StringParsable Empty => new StringParsable();

    public StringParsable()
    {
        Content = ReadOnlyMemory<char>.Empty;
    }

    public StringParsable(string content)
    {
        Content = content.AsMemory();
    }

    public ReadOnlyMemory<char> Content { get; init; }
    public int Offset { get; init; }
    public int PrecedingLines { get; init; }
    public int PrecedingColumns { get; init; }

    public bool IsEmpty()
    {
        return Content.IsEmpty;
    }

    public Match MatchRegex(string pattern, RegexOptions options = RegexOptions.None)
    {
        return Regex.Match(Content.ToString(), pattern, options);
    }

    char IContentVisitable<char>.VisitContent()
    {
        return Content.Slice(0, 1).ToArray()[0];
    }

    string IContentVisitable<string>.VisitContent()
    {
        return Content.ToString();
    }

    public (StringParsable first, StringParsable second) SplitAtPosition(int position)
    {
        var firstContent = position == 0
            ? ReadOnlyMemory<char>.Empty
            : Content.Slice(0, position);
        
        var (firstContainingLines, firstLastColumns) = CalculateLinesAndLastColumns(firstContent);

        var secondContent = position == 0
            ? Content
            : Content.Slice(position);

        var first = new StringParsable
        {
            Content = firstContent,
            Offset = Offset,
            PrecedingLines = PrecedingLines,
            PrecedingColumns = PrecedingColumns,
        };

        var second = new StringParsable
        {
            Content = secondContent,
            Offset = Offset + firstContent.Length,
            PrecedingLines = PrecedingLines + firstContainingLines,
            PrecedingColumns = PrecedingColumns + firstLastColumns,
        };

        return (first, second);
    }

    public StringParsable CreateEmptyTail()
    {
        var (containingLines, lastColumns) = CalculateLinesAndLastColumns(Content);

        return new StringParsable
        {
            Offset = Offset + Content.Length,
            PrecedingLines = PrecedingLines + containingLines,
            PrecedingColumns = PrecedingColumns + lastColumns,
        };
    }

    public T DecorateEmptyTail<T>(T remainderMutator) where T : IRemainderMutator
    {
        var (containingLines, lastColumns) = CalculateLinesAndLastColumns(Content);
        
        remainderMutator.SetOffset(Offset + Content.Length);
        remainderMutator.SetPrecedingLines(PrecedingLines + containingLines);
        remainderMutator.SetPrecedingColumns(PrecedingColumns + lastColumns);

        return remainderMutator;
    }

    public (int ContainingLines, int LastColumns) CalculateLinesAndLastColumns(ReadOnlyMemory<char> source)
    {
        if (source.IsEmpty)
        {
            return (0, 0);
        }

        var sourceSpan = source.Span;

        int lineEndingCount = 0;
        int lastColumnsCount = 0;

        for (int i = 0; i < sourceSpan.Length; i++)
        {
            if (sourceSpan[i] == '\n')
            {
                lastColumnsCount = 0;
                lineEndingCount++;
            }
            else if (i < sourceSpan.Length - 1 && sourceSpan[i] == '\r' && sourceSpan[i + 1] == '\n')
            {
                lastColumnsCount = 0;
                lineEndingCount++;
                i++;
            }
            else
            {
                lastColumnsCount++;
            }
        }

        return (lineEndingCount, lastColumnsCount);
    }

    public int GetContentLength()
    {
        return Content.Length;
    }

    public IEnumerable<char> EnumerateContent()
    {
        return MemoryMarshal.ToEnumerable(Content);
    }

    public static implicit operator StringParsable(string value)
    {
        return value.AsParsable();
    }

    public static implicit operator string(StringParsable parsable)
    {
        return parsable.Content.ToString();
    }

    public static string operator +(StringParsable left, StringParsable right)
    {
        return left.Content.ToString() + right.Content.ToString();
}
}

public interface IParsable
{
}

public interface IRegexMatchable
{
    Match MatchRegex(string pattern, RegexOptions options = RegexOptions.None);
}

public interface IEmptyCheckable
{
    bool IsEmpty();
}

public interface IWithContent<T>
{
    T Content { get; }
}

public interface IContentEnumerable<T>
{
    IEnumerable<T> EnumerateContent();
}

public interface IContentVisitable<T>
{
    T VisitContent();
}

public interface IRemainder
{
    int Offset { get; }
    int PrecedingLines { get; }
    int PrecedingColumns { get; }
}

public interface IRemainderMutator
{
    void SetOffset(int value);
    void SetPrecedingLines(int value);
    void SetPrecedingColumns(int value);
}

public interface ISplitableAtPosition<T>
{
    (T first, T second) SplitAtPosition(int position);
}

public interface IEmptyTailParsableFactory<TSelf>
{
    TSelf CreateEmptyTail();
}

public interface IEmptyTailParsableDecorator
{
    T DecorateEmptyTail<T>(T remainder) where T : IRemainderMutator;
}

public interface IContentMeasurable
{
    int GetContentLength();
}
