/// <summary>
/// Severity level of a log message.
/// See each value for examples of when to use them.
/// </summary>
/// <remarks>
/// The name is `LogLvl` to avoid naming conflicts with Unity's `LogLevel`.
/// </remarks>
public enum LogLvl
{
    /// <summary>
    /// Used for debugging, typically will not be available in Release.<br/>
    /// Useful for development or investigation.
    /// <example>
    /// When to use:
    /// - Internal state and variables: "Player HP before calculation: 42"
    /// - Detailed flow: "Entering CalculateDamage() with crit=true"
    /// - Very frequent events: "Physics tick executed"
    /// </example>
    /// </summary>
    DEBUG,

    /// <summary>
    /// Informational messages about relevant execution flow.
    /// <example>
    /// When to use:
    /// - Initializations: "Combat system initialized"
    /// - Player actions: "Card 'Fireball' played"
    /// - Important state changes: "Phase changed to BattlePhase"
    /// </example>
    /// </summary>
    INFO,

    /// <summary>
    /// Unexpected situations that do not prevent the game from running.
    /// <example>
    /// - Fallbacks: "Sprite not found, using placeholder"
    /// - Tolerable inconsistent data: "Negative HP detected, normalizing to 0"
    /// - Incorrect API usage: "Subscribe called twice for the same handler"
    /// </example>
    /// </summary>
    /// <remarks>
    /// In case of exceptions, always pass the exception for context.
    /// </remarks>
    WARN,

    /// <summary>
    /// Failures that compromise functionality or indicate a bug.<br/>
    /// <example>
    /// - Exceptions: "Failed to load deck"
    /// - Critical invalid state: "GameState null during execution"
    /// - Missing mandatory resources: "Camera prefab not configured"
    /// </example>
    /// </summary>
    /// <remarks>
    /// In case of exceptions, always pass the exception for context.
    /// </remarks>
    ERROR,
}
