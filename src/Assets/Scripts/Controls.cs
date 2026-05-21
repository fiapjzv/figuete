public partial class Controls : GameBehavior
{
    private PlayerInputActions _input = null!;

    protected override void Init()
    {
        _input = new PlayerInputActions();
        BindRailShooterEvents(_input.RailShooter);
    }

    protected override void WhenEnabled()
    {
        // TODO: assign when scene is loaded
        _input.RailShooter.Enable();
    }

    protected override void WhenDisabled()
    {
        _input.RailShooter.Disable();
    }
}
