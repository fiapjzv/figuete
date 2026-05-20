public sealed class NullLogger : IGameLogger
{
    public IMsgLogger? Debug => null;

    public IMsgLogger? Info => null;

    public IExceptionMsgLogger? Error => null;

    public IExceptionMsgLogger? Warn => null;

    private NullLogger() { }

    public static IGameLogger Instance { get; } = new NullLogger();
}
