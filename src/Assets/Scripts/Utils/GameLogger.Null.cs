public sealed class NullGameLogger : IGameLogger
{
    public IMsgLogger? Debug => null;

    public IMsgLogger? Info => null;

    public IExceptionMsgLogger? Error => null;

    public IExceptionMsgLogger? Warn => null;

    private NullGameLogger() { }

    public static IGameLogger Instance { get; } = new NullGameLogger();
}
