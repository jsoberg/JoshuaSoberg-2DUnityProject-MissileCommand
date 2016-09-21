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
}
