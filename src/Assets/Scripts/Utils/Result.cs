using System.Diagnostics.CodeAnalysis;

/// <inheritdoc cref="Result{T}" />
public readonly struct Result
{
    /// <summary>Creates an operation that was a success and returned <paramref name="value" />.</summary>
    public static Result<T> Ok<T>(T value) => new(value, error: null);

    /// <summary>Creates an operation that was a failure and returned the <paramref name="error" />.</summary>
    public static Result<T> Err<T>(string error) => new(value: default!, error);

    /// <inheritdoc cref="Result{T}" />
    public static Result<Unit> Ok() => new(default, error: null);

    /// <inheritdoc cref="Err{T}(string)" />
    public static Result<Unit> Err(string error) => new(value: default, error);
}

/// <summary>
/// Represents the result of an operation that can either succeed and have a value or fail and have an error.
/// </summary>
/// <remarks>It can never be in both states (success and error) simultaneously.</remarks>
public readonly struct Result<T>
{
    private readonly T _value;
    private readonly string? _error;
    private readonly bool _isOk;

    internal Result(T value, string? error)
    {
        _isOk = error is null;

        if (_isOk)
        {
            _value = value;
            _error = null;
        }
        else
        {
            _value = default!;
            _error = error ?? throw new System.ArgumentNullException(nameof(error));
        }
    }

    /// <summary>
    /// Checks if the result is a success, returning:
    /// - true in case of success: value is populated, error is null.
    /// - false in case of error: value is default, error is populated.
    /// </summary>
    public bool IsOk([NotNullWhen(returnValue: true)] out T? value, [NotNullWhen(returnValue: false)] out string? error)
    {
        if (_isOk)
        {
            (value, error) = (_value!, null);
            return true;
        }

        (value, error) = (default, _error!);
        return false;
    }

    /// <inheritdoc />
    public override string ToString() => _isOk ? $"Ok({_value})" : $"Err({_error})";
}

public static class ResultExtensions
{
    /// <summary>Converts an exception into an error Result.</summary>
    public static Result<T> AsResult<T>(this System.Exception ex, bool msgOnly = false) =>
        Result.Err<T>(msgOnly ? ex.Message : ex.ToString());

    /// <summary>Converts a string into an error Result.</summary>
    public static Result<T> AsResult<T>(this string error) => Result.Err<T>(error);

    /// <summary>Indicates whether the result is a success, ignoring the value or error.</summary>
    public static bool IsOk<T>(this Result<T> result) => result.IsOk(out _, out _);

    /// <summary>Returns the value if it is a success.</summary>
    /// <remarks>
    /// UNSAFE METHOD (throws an Exception if it is not a success). Only use if you are certain it was successful.
    /// </remarks>
    public static T Unwrap<T>(this Result<T> result) =>
        result.IsOk(out var value, out _) ? value : throw new System.InvalidOperationException("No value present");
}
