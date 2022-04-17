using System.Text.RegularExpressions;

namespace Nom;

public record StringParsable : IParsable
    , IWithContent<string>
    , ISplitableAtPosition<StringParsable>
    , IRegexMatchable
    , IContentVisitable<string>
    , IContentVisitable<char>
    , IEmptyTailParsableFactory<StringParsable>
    , IEmptyTailParsableDecorator
    , IContentMeasurable
    , IContentEnumerable<char>
{
    public StringParsable()
    {
        Content = string.Empty;
    }
    
    public StringParsable(string content)
    {
        Content = content;
    }

    public string Content { get; init; }
    public int Offset { get; init; }
    public int PrecedingLines { get; init; }

    public bool IsEmpty()
    {
        return string.IsNullOrEmpty(Content);
    }

    public Match MatchRegex(string pattern, RegexOptions options = RegexOptions.None)
    {
        return Regex.Match(Content, pattern, options);
    }

    char IContentVisitable<char>.VisitContent()
    {
        return Content[0];
    }

    string IContentVisitable<string>.VisitContent()
    {
        return Content;
    }

    public (StringParsable first, StringParsable second) SplitAtPosition(int position)
    {
        var firstContent = position == 0
            ? string.Empty
            : Content.Substring(0, position);

        var firstInnerLines = firstContent.Length > 0 
            ? Regex.Matches(firstContent, @"(\r\n|\n)").Count
            : 0;

        var secondContent = position == 0
            ? Content
            : Content.Substring(position);

        var first = new StringParsable
        {
            Content = firstContent,
            Offset = Offset,
            PrecedingLines = PrecedingLines,
        };

        var second = new StringParsable
        {
            Content = secondContent,
            Offset = Offset + firstContent.Length,
            PrecedingLines = PrecedingLines + firstInnerLines,
        };

        return (first, second);
    }

    public StringParsable CreateEmptyTail()
    {
        var containingLines = GetContainingLines();

        return new StringParsable
        {
            Offset = Offset + Content.Length,
            PrecedingLines = PrecedingLines + containingLines,
        };
    }

    public T DecorateEmptyTail<T>(T remainderMutator) where T : IRemainderMutator
    {
        remainderMutator.SetOffset(Offset + Content.Length);
        remainderMutator.SetPrecedingLines(PrecedingLines + GetContainingLines());
        return remainderMutator;
    }

    public int GetContainingLines() => Content.Length > 0
        ? Regex.Matches(Content, @"(\r\n|\n)").Count
        : 0;

    public int GetContentLength()
    {
        return Content.Length;
    }

    public IEnumerable<char> EnumerateContent()
    {
        return Content.AsEnumerable();
    }
}

public interface IParsable : IEmptyCheckable, IRemainder
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
}

public interface IRemainderMutator
{
    void SetOffset(int value);
    void SetPrecedingLines(int value);
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
