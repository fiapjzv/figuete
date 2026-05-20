/// <summary>
/// Separated log service for potential extensibility and telemetry.<br/>
/// <b>WARNING:</b> properties are nullable for lower memory allocation in Release (when Debug will be disabled).
/// <example>
/// Usage example:
/// <code>
/// logger.Debug?.Log("Hello world");
/// logger.Error?.Log("This is an error", exception);
/// </code>
/// </example>
/// </summary>
/// <remarks>
/// The name is `IGameLogger` to avoid naming conflicts with Unity's `ILogger`.
/// </remarks>
public interface IGameLogger
{
    /// <inheritdoc cref="LogLvl.DEBUG" />
    IMsgLogger? Debug { get; }

    /// <inheritdoc cref="LogLvl.INFO" />
    IMsgLogger? Info { get; }

    /// <inheritdoc cref="LogLvl.WARN" />
    IExceptionMsgLogger? Warn { get; }

    /// <inheritdoc cref="LogLvl.ERROR" />
    IExceptionMsgLogger? Error { get; }
}

/// <inheritdoc cref="IGameLogger"/>
public interface IMsgLogger
{
    /// <inheritdoc cref="IGameLogger"/>
    void Log(string message);
}

/// <inheritdoc cref="IGameLogger"/>
public interface IExceptionMsgLogger : IMsgLogger
{
    /// <inheritdoc cref="IGameLogger"/>
    void Log(string message, System.Exception ex);
}
