using UnityEngine;
using System.Collections;

public class EnemyMissileController : MissileController 
{
	new protected bool HasReachedTarget()
	{
		return (transform.position.x - TargetPosition.x) <= MinimumTargetProximity;
	}

	void LateUpdate() 
	{
		Vector3[] vertices = { transform.position, StartPosition, };
		GetComponent<LineRenderer> ().SetPositions (vertices);
	}

    protected override void OnStart()
    {
        Object[] enemies = Object.FindObjectsOfType(typeof(Enemy));
        foreach (Enemy e in enemies)
        {
            if (e.gameObject.GetComponent<Collider2D>() != null)
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), e.gameObject.GetComponent<Collider2D>());
            }
        }
    }
}
