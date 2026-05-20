using System;

public static class GameLoggerExtensions
{
    /// <summary>Logs the <see cref="Result"/> of an operation.</summary>
    public static void LogResult<T>(
        this IGameLogger logger,
        Result<T> result,
        LogLvl successLvl = LogLvl.INFO,
        LogLvl errorLvl = LogLvl.ERROR
    ) => DoLogResult(logger, mask: null, result, successLvl, errorLvl);

    /// <summary>Logs the <see cref="Result"/> of an operation using a String.Format mask.</summary>
    public static void LogResult<T>(
        this IGameLogger logger,
        string mask,
        Result<T> result,
        LogLvl successLvl = LogLvl.INFO,
        LogLvl errorLvl = LogLvl.ERROR
    ) => DoLogResult(logger, mask, result, successLvl, errorLvl);

    private static void DoLogResult<T>(
        IGameLogger logger,
        string? mask,
        Result<T> result,
        LogLvl successLvl = LogLvl.INFO,
        LogLvl errorLvl = LogLvl.ERROR
    )
    {
        var logLvl = result.IsOk() ? successLvl : errorLvl;

        var msgLogger = logLvl switch
        {
            LogLvl.DEBUG => logger.Debug,
            LogLvl.INFO => logger.Info,
            LogLvl.WARN => logger.Warn,
            LogLvl.ERROR => logger.Error,
            _ => throw new ArgumentOutOfRangeException(logLvl.ToString()),
        };

        if (msgLogger is null)
        {
            return;
        }

        var msg = mask is null ? result.ToString() : string.Format(mask, result);
        msgLogger.Log(msg);
    }
}
