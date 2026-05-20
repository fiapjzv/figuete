using System;
using UnityEngine.UIElements;

/// <summary>
/// Centralizes checks that guarantee assumptions. The goal is to facilitate debugging. For example:
/// <ul>
/// <li>Ensure that an element exists in the UI and has not changed its name.</li>
/// <li>Ensure that a component is linked in the Unity Editor.</li>
/// </ul>
/// </summary>
public static class Guard
{
    /// <summary>
    /// Ensures that the object is not null.<br/>
    /// If it is null, logs the error and triggers a panic.
    /// </summary>
    /// <remarks>Defensive programming against the billion-dollar mistake.</remarks>
    public static T NotNull<T>(T? obj, IGameLogger logger)
        where T : class
    {
        return obj
            ?? throw Panic(
                $"Expected {typeof(T).Name} not to be null. You might have forgotten to link a component in Unity."
            );
    }

    /// <summary>Ensures that the text is not null or empty.</summary>
    public static string NotEmpty(string param, IGameLogger logger)
    {
        return !string.IsNullOrEmpty(param)
            ? param
            : throw Panic(
                "Expected param cannot to be empty. You might have forgotten to link a component in Unity."
            );
    }

    /// <summary>
    /// Ensures that an element exists within the UI Toolkit hierarchy.
    /// Searches by name and validates if the element was found.
    /// </summary>
    public static VisualElement ElementIsPresent<T>(
        VisualElement root,
        string elementName,
        IGameLogger logger
    )
        where T : VisualElement
    {
        var element = root.Q<T>(elementName);

        return element ?? throw Panic($"Could not find element '{elementName}' on '{root.name}'");
    }

    /// <inheritdoc cref="ElementIsPresent{T}(VisualElement, string, IGameLogger)" />
    public static VisualElement ElementIsPresent(
        VisualElement root,
        string elementName,
        IGameLogger logger
    )
    {
        return ElementIsPresent<VisualElement>(root, elementName, logger);
    }

    [System.Diagnostics.DebuggerHidden]
    public static Exception Panic(string message)
    {
        UnityEngine.Debug.LogError($"FATAL PANIC: {message}");

#if UNITY_EDITOR
        UnityEngine.Debug.Break();
        // NOTE: telling the Unity editor to stop the player if running on editor
        UnityEditor.EditorApplication.delayCall += UnityEditor.EditorApplication.ExitPlaymode;
#else
        Environment.FailFast(message);
#endif
        return new Exception(message);
    }

    public static void Assert(Func<Result<Unit>> check)
    {
        var result = check();
        if (result.IsOk(out _, out var error))
        {
            return;
        }

        throw Panic(error);
    }
}
