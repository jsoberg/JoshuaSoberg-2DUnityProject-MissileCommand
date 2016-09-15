using UnityEngine;
using System.Collections;

public class MissileMovement : MonoBehaviour 
{
	public GameObject ExplosionPrefab;
	public float MinimumTargetProximity = 1;
	public float InitialSpeed = 1;
	public float AcceleratePerSecond = 1;

	public Vector2 TargetPosition;

	void Start () 
	{
		float diffX = TargetPosition.x - transform.position.x;
		float diffY = TargetPosition.y - transform.position.y;
		float dist = Vector3.Distance(TargetPosition, transform.position);
		float a = Mathf.Acos(diffX / dist);

		GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(a * Mathf.Rad2Deg - 90, Vector3.forward) * Vector3.up * InitialSpeed;
		transform.rotation = Quaternion.AngleAxis(a * Mathf.Rad2Deg - 90, Vector3.forward);
	}

	void Update () 
	{
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (GetComponent<Rigidbody2D>().velocity * AcceleratePerSecond * Time.deltaTime);

		if (Vector2.Distance(transform.position, TargetPosition) <  MinimumTargetProximity) {
			Explode();
		}
	}

	private void Explode()
	{
		Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}
}
