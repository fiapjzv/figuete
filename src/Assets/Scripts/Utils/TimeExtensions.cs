using System;
using System.Threading.Tasks;

public static class TimeExtensions
{
    /// <summary>Converts a number into elapsed milliseconds.</summary>
    public static TimeSpan Millis(this int number)
    {
        return TimeSpan.FromMilliseconds(number);
    }

    /// <summary>Converts a number into elapsed seconds.</summary>
    public static TimeSpan Seconds(this int number)
    {
        return TimeSpan.FromSeconds(number);
    }

    /// <summary>Creates an async delay.</summary>
    /// <example>
    /// <code>
    /// await 50.Millis().Delay();
    /// </code>
    /// </example>
    public static Task Delay(this TimeSpan time)
    {
        return Task.Delay(time);
    }
}
