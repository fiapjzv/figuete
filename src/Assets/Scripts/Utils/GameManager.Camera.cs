using UnityEngine;

public partial class GameManager
{
    private Camera _defaultCam = null!;

    private void SetupDefaultCam(Camera camPrefab, IGameLogger logger)
    {
        Guard.Assert(() => ListenerIsDisabled(camPrefab));
        var cam = Instantiate(camPrefab);

        if (!ColorUtility.TryParseHtmlString(CLEAR_SCREEN_COLOR, out var color))
        {
            logger.Error?.Log(
                $"{nameof(CLEAR_SCREEN_COLOR)} must be a valid RGB color! Invalid value: {CLEAR_SCREEN_COLOR}"
            );
            return;
        }
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = color;

        cam.tag = "MainCamera";
        _defaultCam = cam;

        logger.Debug?.Log("Default camera setup complete!");
    }

    private void AttachDefaultCam(IGameLogger logger)
    {
        var sceneCam = FindAnyObjectByType<Camera>();
        if (sceneCam is not null)
        {
#if !DEBUG
            Guard.Panic("The game cannot start with a camera in Release mode!");
#endif
            logger.Warn?.Log("Game starting with custom camera!");
        }
        else
        {
            logger.Debug?.Log("Using default camera");
            sceneCam = _defaultCam;
        } 

        // NOTE: adding the camera inside the GameManager object so it doesn't get destroyed
        sceneCam.transform.SetParent(transform);

        if (sceneCam.TryGetComponent<AudioListener>(out var listener))
        {
            listener.enabled = true;
        }
    }
    // NOTE: we expected that the default cam have a disabled audio listener to avoid the annoying error
    //       "There are 2 audio listeners in the scene." when we are debugging a scene directly on the editor
    private static Result<Unit> ListenerIsDisabled(Camera camPrefab)
    {
        if (!camPrefab.TryGetComponent<AudioListener>(out var listener))
        {
            return Result.Err($"Default camera prefab does not have a {nameof(AudioListener)}.");
        }

        return listener.enabled
            ? Result.Err($"We expect that the listener on the default camera is disabled on Awake")
            : Result.Ok();
    }
}
