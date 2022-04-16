using Nom.Characters;
using Xunit;

namespace Nom.Core.UnitTests.Characters;

public class AlphabetTests
{
    [Fact]
    public void AlphabetParsesAlphabetics()
    {
        var parser = Alphabet.Create();

        var result = parser.Parse("abc1abc");

        Assert.Equal(Result.Create("1abc", "abc"), result);
    }
}
