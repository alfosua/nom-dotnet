namespace Nom;

public interface IParser { }

public interface IParser<TInput, TOutput> : IParser
{
    IResult<TInput, TOutput> Parse(TInput input);
}
