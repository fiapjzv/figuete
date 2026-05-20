using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>Text internationalization service.</summary>
public interface I18n
{
    /// <summary>Current game language.</summary>
    Lang Lang { get; }

    /// <summary>Synchronously loads as many labels as possible.</summary>
    void Start(string[] mandatorySections);

    /// <summary>
    /// Contacts the server to fetch a group of keys and translations <see cref="I18nSection" />.
    /// </summary>
    Task<Result<I18nSection>> ForSection(string sectionKey);
}

/// <summary>
/// Contacts the server or a local file cache to load translations in the player's language.
/// </summary>
public partial class I18nImpl : I18n
{
    /// <inheritdoc/>
    public Lang Lang { get; private set; }

    private readonly IGameLogger _logger;
    private readonly Dictionary<string, I18nSection> _loadedSections = new();

    public I18nImpl(IEvents events, IGameLogger? logger = null)
    {
        // TODO: get player locale
        Lang = Lang.PT_BR;
        _logger = logger ?? NullLogger.Instance;
    }

    // TODO: change locale with event
    // /// <inheritdoc/>
    // public void ChangeLocale(string locale)
    // {
    //      // TODO: reset local cache
    //      var error = $"Implement {nameof(I18nImpl)}.{nameof(ChangeLocale)}!";
    //      _logger.Error?.Log(error);
    //      throw new NotImplementedException(error);
    // }
}
