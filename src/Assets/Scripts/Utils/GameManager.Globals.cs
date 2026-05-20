using UnityEngine;

public partial class GameManager
{
    /// <summary>Returns the target FPS of the game.</summary>
    /// <remarks>
    /// This value is only an intended frame rate; it does not guarantee that the game is actually running at this FPS.
    /// </remarks>
    public static int FramesPerSecond()
    {
        return Application.targetFrameRate > 0 ? Application.targetFrameRate : 60;
    }

    /// <summary>Returns the target time per frame in milliseconds.</summary>
    /// <remarks>
    /// This value is based on an intended frame rate; it does not guarantee that the game is actually running at this FPS.
    /// </remarks>
    public static long FrameBudgetInMs()
    {
        return 1000L / FramesPerSecond();
    }

    // TODO: unify the UI palette (on the uss) with the game palette using a helper method like:
    //       `root.style.SetProperty("--color-bg-main", Palette.BgMainColor);`
    /// <summary>Which color to show when nothing is being displayed on the screen.</summary>
    public const string CLEAR_SCREEN_COLOR = "#121212";

    public static class DepthLayers
    {
        public const float CAMERA_GLOBAL_Z = -10f;
        public const float TABLE_ZONE_Z = 0f;
    }

    /// <summary>Viewport height in world units using the default camera orthographic projection</summary>
    public const float VIEWPORT_HEIGHT = 14f;
}
