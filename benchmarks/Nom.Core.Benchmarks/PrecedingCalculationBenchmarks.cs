using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Text.RegularExpressions;

[RankColumn]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[MemoryDiagnoser]
public class PrecedingCalculationBenchmarks
{
    private static ReadOnlyMemory<char> source = "some\nline\r\nlol\n\r\n\nsome predeceding columns".AsMemory();

    [Benchmark]
    public (int, int) UsingImperativeFor()
    {
        if (source.IsEmpty)
        {
            return (0, 0);
        }

        var sourceSpan = source.Span;

        int lineEndingCount = 0;
        int lastColumnsCount = 0;

        for (int i = 0; i < sourceSpan.Length; i++)
        {
            if (sourceSpan[i] == '\n')
            {
                lastColumnsCount = 0;
                lineEndingCount++;
            }
            else if (i < sourceSpan.Length - 1 && sourceSpan[i] == '\r' && sourceSpan[i + 1] == '\n')
            {
                lastColumnsCount = 0;
                lineEndingCount++;
                i++;
            }
            else
            {
                lastColumnsCount++;
            }
        }

        return (lineEndingCount, lastColumnsCount);
    }

    [Benchmark]
    public (int, int) UsingRegexMatch()
    {
        if (source.IsEmpty)
        {
            return (0, 0);
        }

        var sourceString = source.ToString();

        var lineEndingMatches = Regex.Matches(sourceString, @"(\r\n|\n)");

        var lastColumnsCount = sourceString.Length - 1 - lineEndingMatches.LastOrDefault()?.Index ?? 0;

        return (lineEndingMatches.Count, lastColumnsCount);
    }
}