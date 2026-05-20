using System;
using UnityDebug = UnityEngine.Debug;

public class UnityLogger : IGameLogger
{
    public IMsgLogger? Debug { get; }
    public IMsgLogger? Info { get; }
    public IExceptionMsgLogger? Warn { get; }
    public IExceptionMsgLogger? Error { get; }

    public UnityLogger(LogLvl lvl)
    {
        Debug = lvl <= LogLvl.DEBUG ? new MsgLogger(UnityDebug.Log, LogLvl.DEBUG) : null;
        Info = lvl <= LogLvl.INFO ? new MsgLogger(UnityDebug.Log, LogLvl.INFO) : null;
        Warn = lvl <= LogLvl.WARN ? new ExMsgLogger(UnityDebug.LogWarning, LogLvl.WARN) : null;
        Error = lvl <= LogLvl.ERROR ? new ExMsgLogger(UnityDebug.LogError, LogLvl.ERROR) : null;

        // _type = GetType().FullName ?? GetType().Name;
    }

    private class MsgLogger : IMsgLogger
    {
        private readonly Action<string> _log;
        private readonly string _levelPrefix;

        public MsgLogger(Action<string> log, LogLvl level)
        {
            _log = log;
            _levelPrefix = level.ToString();
        }

        public void Log(string message)
        {
            _log($"[{_levelPrefix}] {message}");
        }
    }

    private class ExMsgLogger : MsgLogger, IExceptionMsgLogger
    {
        public ExMsgLogger(Action<string> log, LogLvl level)
            : base(log, level) { }

        public void Log(string message, Exception ex)
        {
            Log(message);
            UnityDebug.LogException(ex);
        }
    }
}
