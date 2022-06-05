namespace Nom.Combinators;

public interface IRestParser<T> : IParser<T, T>
    where T : IParsable, new()
{
}

public class RestParser<T> : IRestParser<T>
    where T : IParsable, new()
{
    public IResult<T, T> Parse(T input)
    {
        var remainder = input is IEmptyTailParsableFactory<T> factory
            ? factory.CreateEmptyTail()
            : new T();

        return Result.Create(remainder, input);
    }
}

public static class Rest
{
    public static IParser<T, T> Create<T>()
        where T : IParsable, new()
    {
        return new RestParser<T>();
    }
}
