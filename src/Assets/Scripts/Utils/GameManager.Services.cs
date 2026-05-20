using System;
using System.Threading;

public partial class GameManager
{
    private static (IEvents, I18n, IGameLogger) SetupBasicServices()
    {
        const LogLvl logLvl =
#if DEBUG
        LogLvl.DEBUG;
#else
        LogLvl.INFO;
#endif
        var logger = new UnityLogger(logLvl);
        var events = new UnityEvents(SynchronizationContext.Current, logger);
        var i18n = new I18nImpl(events, logger);
        var scenes = new Scenes(events, logger);

        Service.Register<IGameLogger>(logger);
        Service.Register<IEvents>(events);
        Service.Register<I18n>(i18n);
        Service.RegisterPrivate(scenes);
        logger.Info?.Log("Setup services complete");
        return (events, i18n, logger);
    }

    private static void SetupGameServices()
    {
        throw new NotImplementedException();
    }
}
