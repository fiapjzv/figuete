using UnityEngine;

public partial class Asteroid
{
    private void OnTriggerEnter(Collider collision)
    {
        Logger.Debug?.Log("OnTriggerEnter");
        if (!collision.TryGetComponent<Asteroid>(out var other))
        {
            return;
        }

        Logger.Debug?.Log("Double Swap");
        // NOTE: Prevent the "Double Swap" trap
        if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID())
        {
            return;
        }

        Events.Publish(new CollisionEvt(this, other));
    }
}
