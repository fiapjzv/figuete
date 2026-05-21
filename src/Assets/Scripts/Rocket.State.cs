using UnityEngine;
using MoveRocketEvent = Controls.RailShooter.MoveRocketEvent;

public partial class Rocket
{
    public enum State
    {
        START_ANIMATION,
        GAMEPLAY,
        GAME_OVER_ANIMATION,
    }
}
