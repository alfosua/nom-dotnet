namespace Nom.Combinators;

public delegate TMapOutput MapDelegate<TParserOutput, TMapOutput>(TParserOutput parserOutput);

public interface IMapParser<TInput, TParserOutput, TMapOutput> : IParser<TInput, TMapOutput>
    where TInput : IParsable
{
}

public class MapParser<TInput, TParserOutput, TMapOutput>
    : IMapParser<TInput, TParserOutput, TMapOutput>
    where TInput : IParsable
{
    public MapParser(IParser<TInput, TParserOutput> parser, MapDelegate<TParserOutput, TMapOutput> mapper)
    {
        Parser = parser;
        Delegate = mapper;
    }

    public IParser<TInput, TParserOutput> Parser { get; set; }
    public MapDelegate<TParserOutput, TMapOutput> Delegate { get; set; }

    public IResult<TInput, TMapOutput> Parse(TInput input)
    {
        var parserResult = Parser.Parse(input);
        var mapOutput = Delegate.Invoke(parserResult.Output);
        return Result.Create(parserResult.Remainder, mapOutput);
    }
}

public static class Map
{
    public static IParser<TInput, TMapOutput>
        Create<TInput, TParserOutput, TMapOutput>(
            IParser<TInput, TParserOutput> parser,
            MapDelegate<TParserOutput, TMapOutput> mapDelegate)
            where TInput : IParsable
    {
        return new MapParser<TInput, TParserOutput, TMapOutput>(parser, mapDelegate);
    }
}
