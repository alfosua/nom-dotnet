using Nom.Characters;
using Nom.Combinators;
using Nom.Sequences;
using Xunit;

namespace Nom.Core.UnitTests.Combinations.Combinators;

public class MapCombinationsTests
{
    record Person
    {
        public string FirstName { get; init; } = "";
        public string LastName { get; init; } = "";
    }

    [Fact]
    public void CombinationParses_TwoWordsParserIntoPerson_AndLeavesRemains()
    {
        var input = "Wanda Maximoff_";
        var expected = new Person
        {
            FirstName = "Wanda",
            LastName = "Maximoff",
        };

        var twoWordsParser = SeparatedPair.Create(Alphabet.Create(), Character.Create(' '), Alphabet.Create());

        var personParser = Map.Create(twoWordsParser, ((string FirstName, string LastName) x) => new Person
        {
            FirstName = x.FirstName,
            LastName = x.LastName,
        });

        var result = personParser.Parse(input);

        Assert.Equal("_", result.Remaining);
        Assert.Equal(expected, result.Output);
    }
}
