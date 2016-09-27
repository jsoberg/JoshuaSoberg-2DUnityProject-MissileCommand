using UnityEngine;
using System.Collections;

public class FriendlyMissileController : MissileController
{
    protected override bool HasReachedTarget()
    {
        // We've reached our target if we reach the height that we initially targeted.
        return base.HasReachedTarget() || (transform.position.y > TargetPosition.y);
    }

    protected override void PostStart()
    {
        Object[] friendlies = Object.FindObjectsOfType(typeof(Friendly));
        foreach (Friendly f in friendlies) {
            if (f.gameObject.GetComponent<Collider2D>() != null) {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), f.gameObject.GetComponent<Collider2D>());
            }
        }
    }
}
