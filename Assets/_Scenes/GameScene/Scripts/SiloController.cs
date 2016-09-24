using UnityEngine;
using System.Collections;

public class SiloController : MonoBehaviour
{
    public Rigidbody2D Missile;

    void Start()
    {

    }

    void Update()
    {

    }

    public void FireMissile(Vector3 target)
    {
        Vector2 center = GetComponent<SpriteRenderer>().bounds.center;
        Vector2 top = GetComponent<SpriteRenderer>().bounds.max;
        Vector2 start = new Vector2(center.x, top.y);
        Rigidbody2D missileClone = (Rigidbody2D)Instantiate(Missile, start, Quaternion.identity);
        missileClone.GetComponent<MissileController>().TargetPosition = target;
    }

}
