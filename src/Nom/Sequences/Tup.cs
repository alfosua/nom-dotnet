namespace Nom.Sequences;

public interface ITupParser<TCommonInput, TO1>
    : IParser<TCommonInput, ValueTuple<TO1>>
    where TCommonInput : IParsable
{
}

public interface ITupParser<TCommonInput, TO1, TO2>
    : IParser<TCommonInput, (TO1, TO2)>
    where TCommonInput : IParsable
{
}

public interface ITupParser<TCommonInput, TO1, TO2, TO3>
    : IParser<TCommonInput, (TO1, TO2, TO3)>
    where TCommonInput : IParsable
{
}

public interface ITupParser<TCommonInput, TO1, TO2, TO3, TO4>
    : IParser<TCommonInput, (TO1, TO2, TO3, TO4)>
    where TCommonInput : IParsable
{
}

public interface ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5>
    : IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5)>
    where TCommonInput : IParsable
{
}

public interface ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6>
    : IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6)>
    where TCommonInput : IParsable
{
}

public interface ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7>
    : IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6, TO7)>
    where TCommonInput : IParsable
{
}

public interface ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8>
    : IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8)>
    where TCommonInput : IParsable
{
}

public class TupParser<TCommonInput, TO1> : ITupParser<TCommonInput, TO1>
    where TCommonInput : IParsable
{
    public TupParser(ValueTuple<IParser<TCommonInput, TO1>> parsers)
        => Parser1 = parsers.Item1;

    public IParser<TCommonInput, TO1> Parser1 { get; }

    public IResult<TCommonInput, ValueTuple<TO1>> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        return Result.Create(result1.Remainder, ValueTuple.Create(result1.Output));
    }
}

public class TupParser<TCommonInput, TO1, TO2> : ITupParser<TCommonInput, TO1, TO2>
    where TCommonInput : IParsable
{
    public TupParser((IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>) parsers)
        => (Parser1, Parser2) = parsers;

    public IParser<TCommonInput, TO1> Parser1 { get; }
    public IParser<TCommonInput, TO2> Parser2 { get; }

    public IResult<TCommonInput, (TO1, TO2)> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        var result2 = Parser2.Parse(result1.Remainder);
        var output = (result1.Output, result2.Output);
        return Result.Create(result2.Remainder, output);
    }
}

public class TupParser<TCommonInput, TO1, TO2, TO3> : ITupParser<TCommonInput, TO1, TO2, TO3>
    where TCommonInput : IParsable
{
    public TupParser((IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>) parsers)
        => (Parser1, Parser2, Parser3) = parsers;

    public IParser<TCommonInput, TO1> Parser1 { get; }
    public IParser<TCommonInput, TO2> Parser2 { get; }
    public IParser<TCommonInput, TO3> Parser3 { get; }

    public IResult<TCommonInput, (TO1, TO2, TO3)> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        var result2 = Parser2.Parse(result1.Remainder);
        var result3 = Parser3.Parse(result2.Remainder);
        var output = (result1.Output, result2.Output, result3.Output);
        return Result.Create(result3.Remainder, output);
    }
}

public class TupParser<TCommonInput, TO1, TO2, TO3, TO4> : ITupParser<TCommonInput, TO1, TO2, TO3, TO4>
    where TCommonInput : IParsable
{    
    public TupParser((IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>) parsers)
        => (Parser1, Parser2, Parser3, Parser4) = parsers;

    public IParser<TCommonInput, TO1> Parser1 { get; }
    public IParser<TCommonInput, TO2> Parser2 { get; }
    public IParser<TCommonInput, TO3> Parser3 { get; }
    public IParser<TCommonInput, TO4> Parser4 { get; }

    public IResult<TCommonInput, (TO1, TO2, TO3, TO4)> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        var result2 = Parser2.Parse(result1.Remainder);
        var result3 = Parser3.Parse(result2.Remainder);
        var result4 = Parser4.Parse(result3.Remainder);
        var output = (result1.Output, result2.Output, result3.Output, result4.Output);
        return Result.Create(result4.Remainder, output);
    }
}

public class TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5> : ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5>
    where TCommonInput : IParsable
{
    public TupParser((IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>) parsers)
        => (Parser1, Parser2, Parser3, Parser4, Parser5) = parsers;

    public IParser<TCommonInput, TO1> Parser1 { get; }
    public IParser<TCommonInput, TO2> Parser2 { get; }
    public IParser<TCommonInput, TO3> Parser3 { get; }
    public IParser<TCommonInput, TO4> Parser4 { get; }
    public IParser<TCommonInput, TO5> Parser5 { get; }

    public IResult<TCommonInput, (TO1, TO2, TO3, TO4, TO5)> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        var result2 = Parser2.Parse(result1.Remainder);
        var result3 = Parser3.Parse(result2.Remainder);
        var result4 = Parser4.Parse(result3.Remainder);
        var result5 = Parser5.Parse(result4.Remainder);
        var output = (result1.Output, result2.Output, result3.Output, result4.Output, result5.Output);
        return Result.Create(result5.Remainder, output);
    }
}

public class TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6> : ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6>
    where TCommonInput : IParsable
{
    public TupParser((IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>, IParser<TCommonInput, TO6>) parsers)
        => (Parser1, Parser2, Parser3, Parser4, Parser5, Parser6) = parsers;

    public IParser<TCommonInput, TO1> Parser1 { get; }
    public IParser<TCommonInput, TO2> Parser2 { get; }
    public IParser<TCommonInput, TO3> Parser3 { get; }
    public IParser<TCommonInput, TO4> Parser4 { get; }
    public IParser<TCommonInput, TO5> Parser5 { get; }
    public IParser<TCommonInput, TO6> Parser6 { get; }

    public IResult<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6)> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        var result2 = Parser2.Parse(result1.Remainder);
        var result3 = Parser3.Parse(result2.Remainder);
        var result4 = Parser4.Parse(result3.Remainder);
        var result5 = Parser5.Parse(result4.Remainder);
        var result6 = Parser6.Parse(result5.Remainder);
        var output = (result1.Output, result2.Output, result3.Output, result4.Output, result5.Output, result6.Output);
        return Result.Create(result6.Remainder, output);
    }
}

public class TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7> : ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7>
    where TCommonInput : IParsable
{
    public TupParser((IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>, IParser<TCommonInput, TO6>, IParser<TCommonInput, TO7>) parsers)
        => (Parser1, Parser2, Parser3, Parser4, Parser5, Parser6, Parser7) = parsers;

    public IParser<TCommonInput, TO1> Parser1 { get; }
    public IParser<TCommonInput, TO2> Parser2 { get; }
    public IParser<TCommonInput, TO3> Parser3 { get; }
    public IParser<TCommonInput, TO4> Parser4 { get; }
    public IParser<TCommonInput, TO5> Parser5 { get; }
    public IParser<TCommonInput, TO6> Parser6 { get; }
    public IParser<TCommonInput, TO7> Parser7 { get; }

    public IResult<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6, TO7)> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        var result2 = Parser2.Parse(result1.Remainder);
        var result3 = Parser3.Parse(result2.Remainder);
        var result4 = Parser4.Parse(result3.Remainder);
        var result5 = Parser5.Parse(result4.Remainder);
        var result6 = Parser6.Parse(result5.Remainder);
        var result7 = Parser7.Parse(result6.Remainder);
        var output = (result1.Output, result2.Output, result3.Output, result4.Output, result5.Output, result6.Output, result7.Output);
        return Result.Create(result7.Remainder, output);
    }
}

public class TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8> : ITupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8>
    where TCommonInput : IParsable
{
    public TupParser((IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>, IParser<TCommonInput, TO6>, IParser<TCommonInput, TO7>, IParser<TCommonInput, TO8>) parsers)
        => (Parser1, Parser2, Parser3, Parser4, Parser5, Parser6, Parser7, Parser8) = parsers;

    public IParser<TCommonInput, TO1> Parser1 { get; }
    public IParser<TCommonInput, TO2> Parser2 { get; }
    public IParser<TCommonInput, TO3> Parser3 { get; }
    public IParser<TCommonInput, TO4> Parser4 { get; }
    public IParser<TCommonInput, TO5> Parser5 { get; }
    public IParser<TCommonInput, TO6> Parser6 { get; }
    public IParser<TCommonInput, TO7> Parser7 { get; }
    public IParser<TCommonInput, TO8> Parser8 { get; }

    public IResult<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8)> Parse(TCommonInput input)
    {
        var result1 = Parser1.Parse(input);
        var result2 = Parser2.Parse(result1.Remainder);
        var result3 = Parser3.Parse(result2.Remainder);
        var result4 = Parser4.Parse(result3.Remainder);
        var result5 = Parser5.Parse(result4.Remainder);
        var result6 = Parser6.Parse(result5.Remainder);
        var result7 = Parser7.Parse(result6.Remainder);
        var result8 = Parser8.Parse(result7.Remainder);
        var output = (result1.Output, result2.Output, result3.Output, result4.Output, result5.Output, result6.Output, result7.Output, result8.Output);
        return Result.Create(result8.Remainder, output);
    }
}

public static class Tup
{
    public static IParser<TCommonInput, ValueTuple<TO1>>
        Create<TCommonInput, TO1>(
            ValueTuple<IParser<TCommonInput, TO1>> parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1>(parsers);
    }
    
    public static IParser<TCommonInput, (TO1, TO2)>
        Create<TCommonInput, TO1, TO2>(
            (IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>) parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1, TO2>(parsers);
    }

    public static IParser<TCommonInput, (TO1, TO2, TO3)>
        Create<TCommonInput, TO1, TO2, TO3>(
            (IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>) parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1, TO2, TO3>(parsers);
    }

    public static IParser<TCommonInput, (TO1, TO2, TO3, TO4)>
        Create<TCommonInput, TO1, TO2, TO3, TO4>(
            (IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>) parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1, TO2, TO3, TO4>(parsers);
    }

    public static IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5)>
        Create<TCommonInput, TO1, TO2, TO3, TO4, TO5>(
            (IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>) parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5>(parsers);
    }

    public static IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6)>
        Create<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6>(
            (IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>, IParser<TCommonInput, TO6>) parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6>(parsers);
    }

    public static IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6, TO7)>
        Create<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7>(
            (IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>, IParser<TCommonInput, TO6>, IParser<TCommonInput, TO7>) parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7>(parsers);
    }

    public static IParser<TCommonInput, (TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8)>
        Create<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8>(
            (IParser<TCommonInput, TO1>, IParser<TCommonInput, TO2>, IParser<TCommonInput, TO3>, IParser<TCommonInput, TO4>, IParser<TCommonInput, TO5>, IParser<TCommonInput, TO6>, IParser<TCommonInput, TO7>, IParser<TCommonInput, TO8>) parsers)
            where TCommonInput : IParsable
    {
        return new TupParser<TCommonInput, TO1, TO2, TO3, TO4, TO5, TO6, TO7, TO8>(parsers);
    }
}
