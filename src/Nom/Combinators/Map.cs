namespace Nom.Combinators;

public delegate TMapOutput MapDelegate<TParserOutput, TMapOutput>(TParserOutput parserOutput);

public interface IMapParser<TInput, TParserOutput, TMapOutput> : IParser<TInput, TMapOutput>
{
}

public class MapParser<TInput, TParserOutput, TMapOutput>
    : IMapParser<TInput, TParserOutput, TMapOutput>
{
    public MapParser(IParser<TInput, TParserOutput> parser, MapDelegate<TParserOutput, TMapOutput> mapDelegate)
    {
        Parser = parser;
        Delegate = mapDelegate;
    }

    public IParser<TInput, TParserOutput> Parser { get; set; }
    public MapDelegate<TParserOutput, TMapOutput> Delegate { get; set; }

    public IResult<TInput, TMapOutput> Parse(TInput input)
    {
        var parserResult = Parser.Parse(input);
        var mapOutput = Delegate.Invoke(parserResult.Output);
        return Result.Create(parserResult.Remaining, mapOutput);
    }
}

public static class Map
{
    public static IMapParser<TInput, TParserOutput, TMapOutput>
        Create<TInput, TParserOutput, TMapOutput>(
            IParser<TInput, TParserOutput> parser,
            MapDelegate<TParserOutput, TMapOutput> mapDelegate)
    {
        return new MapParser<TInput, TParserOutput, TMapOutput>(parser, mapDelegate);
    }
}
