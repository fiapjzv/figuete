using static Controls.RailShooter.MoveRocketEvent;

public partial class Controls
{
    private void BindRailShooterEvents(PlayerInputActions.RailShooterActions actions)
    {
        actions.MoveUp.performed += _ => Events.Publish(UP);
        actions.MoveDown.performed += _ => Events.Publish(DOWN);
        actions.MoveLeft.performed += _ => Events.Publish(LEFT);
        actions.MoveRight.performed += _ => Events.Publish(RIGHT);
    }

    public class RailShooter
    {
        public enum MoveRocketEvent
        {
            UP,
            DOWN,
            LEFT,
            RIGHT,
        }
    }
}
