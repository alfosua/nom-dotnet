using Nom.Characters;
using Nom.Collections;
using Nom.Combinators;
using Nom.Sequences;
using Nom.Strings;
using System.Collections.Generic;
using Xunit;

namespace Nom.Core.UnitTests.Combinations.CaseSpecific;

public class LinesTest
{
    public record Point(int x, int y);
    public record Line(Point p0, Point p1);

    public IParser<StringParsable, Point> CreatePointParser()
    {
        var numberParser = Map.Create(Digits.Create(), (n) => int.Parse(n.Content));
        var twoNumbersParser = SeparatedPair.Create(numberParser, Character.Create(','), numberParser);
        var pointParser = Map.Create(twoNumbersParser, ((int x, int y) p) => new Point(p.x, p.y));
        return pointParser;
    }

    public IParser<StringParsable, Line> CreateLineParser()
    {
        var pointParser = CreatePointParser();
        var arrowParser = Tag.Create(" -> ");
        var twoPointsParser = SeparatedPair.Create(pointParser, arrowParser, pointParser);
        var lineParser = Map.Create(twoPointsParser, ((Point p0, Point p1) l) => new Line(l.p0, l.p1));
        return lineParser;
    }

    public IParser<StringParsable, ICollection<Line>> CreateLinesParser()
    {
        var lineParser = CreateLineParser();
        var linesParser = SeparatedList.Create(LineEnding.Create(), lineParser);
        return linesParser;
    }

    [Fact]
    public void TestPointParser()
    {
        var tests = new[]
        {
            ("1,2", new Point(1, 2), ""),
            ("5,6asd", new Point(5, 6), "asd"),
        };

        var pointParser = CreatePointParser();

        foreach (var (input, expected, expectedRem) in tests)
        {
            var result = pointParser.Parse(input.AsParsable());
            Assert.Equal(expected, result.Output);
            Assert.Equal(expectedRem, result.Remainder.Content);
        }
    }

    [Fact]
    public void TestLineParser()
    {
        var tests = new[]
        {
            ("1,2 -> 5,6", new Line(new(1, 2), new(5, 6)), ""),
            ("0,20 -> 445,56asd", new Line(new(0, 20), new(445, 56)), "asd"),
        };

        var lineParser = CreateLineParser();

        foreach (var (input, expected, expectedRem) in tests)
        {
            var result = lineParser.Parse(input.AsParsable());
            Assert.Equal(expected, result.Output);
            Assert.Equal(expectedRem, result.Remainder.Content);
        }
    }

    [Fact]
    public void TestLinesParser()
    {
        var input = @"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2".AsParsable();
        
        var expected = new List<Line>
        {
            new Line(new(0, 9), new(5, 9)),
            new Line(new(8, 0), new(0, 8)),
            new Line(new(9, 4), new(3, 4)),
            new Line(new(2, 2), new(2, 1)),
            new Line(new(7, 0), new(7, 4)),
            new Line(new(6, 4), new(2, 0)),
            new Line(new(0, 9), new(2, 9)),
            new Line(new(3, 4), new(1, 4)),
            new Line(new(0, 0), new(8, 8)),
            new Line(new(5, 5), new(8, 2)),
        };

        var linesParser = CreateLinesParser();

        var result = linesParser.Parse(input);

        Assert.Equal(expected, result.Output);
        Assert.Equal("", result.Remainder.Content);
    }
}
