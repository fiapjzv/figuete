public partial class Rocket
{
    private void MoveRocket(Controls.RailShooter.MoveRocketEvent evt)
    {
        Logger.Debug?.Log($"Moving rocket {evt}");
    }
}
