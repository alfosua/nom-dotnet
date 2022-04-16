﻿namespace Nom;

public interface IResult<TInput, TOutput>
{
    TInput Remaining { get; }
    TOutput Output { get; }

    void Deconstruct(out TInput remaining, out TOutput output);
}

public record Result<TInput, TOutput> : IResult<TInput, TOutput>
{
    public Result(TInput input, TOutput output)
    {
        Remaining = input;
        Output = output;
    }

    public TInput Remaining { get; }
    public TOutput Output { get; }

    public void Deconstruct(out TInput input, out TOutput output)
    {
        output = Output;
        input = Remaining;
    }

    public static implicit operator (TInput input, TOutput output)(Result<TInput, TOutput> result)
    {
        return (result.Remaining, result.Output);
    }

    public static implicit operator Result<TInput, TOutput>((TInput Input, TOutput Output) tuple)
    {
        return new Result<TInput, TOutput>(tuple.Input, tuple.Output);
    }
}

public static class Result
{
    public static Result<TInput, TOutput> Create<TInput, TOutput>(TInput input, TOutput output)
    {
        return new Result<TInput, TOutput>(input, output);
    }
}
