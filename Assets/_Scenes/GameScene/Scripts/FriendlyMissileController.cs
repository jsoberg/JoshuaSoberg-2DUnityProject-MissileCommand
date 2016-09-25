using UnityEngine;
using System.Collections;

public class FriendlyMissileController : MissileController
{
    protected override void OnStart()
    {
        Object[] friendlies = Object.FindObjectsOfType(typeof(Friendly));
        foreach (Friendly f in friendlies) {
            if (f.gameObject.GetComponent<Collider2D>() != null) {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), f.gameObject.GetComponent<Collider2D>());
            }
        }
    }
}
