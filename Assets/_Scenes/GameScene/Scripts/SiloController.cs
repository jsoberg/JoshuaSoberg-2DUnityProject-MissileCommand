using UnityEngine;
using System.Collections;

public class SiloController : MonoBehaviour
{
    public GameObject MissileSilo;
    public GameObject BurntMissileSilo;
    public GameObject BurningParticleSystem;
    public Rigidbody2D Missile;

    public bool IsDestroyed;

    void Start()
    {
        if (IsDestroyed) {
            Destroy();
        }
    }

    void Update()
    {

    }

    public void FireMissile(Vector3 target)
    {
        Vector2 center = MissileSilo.GetComponent<SpriteRenderer>().bounds.center;
        Vector2 top = MissileSilo.GetComponent<SpriteRenderer>().bounds.max;
        Vector2 start = new Vector2(center.x, top.y + 4);
        Rigidbody2D missileClone = (Rigidbody2D)Instantiate(Missile, start, Quaternion.identity);
        missileClone.GetComponent<MissileController>().TargetPosition = target;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy();
    }

    private void Destroy()
    {
        IsDestroyed = true;
        MissileSilo.SetActive(false);
        BurntMissileSilo.SetActive(true);
        BurningParticleSystem.SetActive(true);
    }
}
