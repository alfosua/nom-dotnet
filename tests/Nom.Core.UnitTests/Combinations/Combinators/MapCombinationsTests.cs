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
        var input = "Wanda Maximoff_".AsParsable();
        var expected = new Person
        {
            FirstName = "Wanda",
            LastName = "Maximoff",
        };

        var twoWordsParser = SeparatedPair.Create(Alphabetics.Create(), Character.Create(' '), Alphabetics.Create());

        var personParser = Map.Create(twoWordsParser, ((StringParsable FirstName, StringParsable LastName) x) => new Person
        {
            FirstName = x.FirstName.Content,
            LastName = x.LastName.Content,
        });

        var result = personParser.Parse(input);

        Assert.Equal("_", result.Remainder.Content);
        Assert.Equal(expected, result.Output);
    }
}
