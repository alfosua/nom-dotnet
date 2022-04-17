namespace Nom.Combinators;

public interface IRestLengthParser<TInput> : IParser<TInput, int>
    where TInput : IParsable, IContentMeasurable
{
}

public class RestLengthParser<TInput> : IRestLengthParser<TInput>
    where TInput : IParsable, IContentMeasurable
{
    public IResult<TInput, int> Parse(TInput input)
    {
        return Result.Create(input, input.GetContentLength());
    }
}

public static class RestLength
{
    public static IRestLengthParser<TInput> Create<TInput>()
        where TInput : IParsable, IContentMeasurable
        => new RestLengthParser<TInput>();
}
