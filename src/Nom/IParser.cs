using System.Text.RegularExpressions;

namespace Nom;

public interface IParser { }

public interface IParser<TInput, TOutput> : IParser
    where TInput : IParsable
{
    IResult<TInput, TOutput> Parse(TInput input);
}
