using UnityEngine;
using System.Collections;

public class MissileMovement : MonoBehaviour 
{
	public GameObject TargetReticule;
	private Object TargetReticuleInstance;
	public GameObject ExplosionPrefab;

	public float MinimumTargetProximity = 1;
	public float InitialSpeed = 1;
	public float AcceleratePerSecond = 1;
	public Vector2 TargetPosition;

	// 3 Seconds
	private const float MAX_TIME_TO_LIVE = 3;
	private float TimeSinceStart;

	void Start () 
	{
		StartTargetAnimation ();

		float diffX = TargetPosition.x - transform.position.x;
		float diffY = TargetPosition.y - transform.position.y;
		float dist = Vector3.Distance(TargetPosition, transform.position);
		float a = Mathf.Acos(diffX / dist);

		GetComponent<Rigidbody2D>().velocity = Quaternion.AngleAxis(a * Mathf.Rad2Deg - 90, Vector3.forward) * Vector3.up * InitialSpeed;
		transform.rotation = Quaternion.AngleAxis(a * Mathf.Rad2Deg - 90, Vector3.forward);
	}

	void StartTargetAnimation()
	{
		TargetReticuleInstance = Instantiate (TargetReticule, TargetPosition, Quaternion.identity);
	}

	void Update () 
	{
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (GetComponent<Rigidbody2D>().velocity * AcceleratePerSecond * Time.deltaTime);

		if (Vector2.Distance(transform.position, TargetPosition) <  MinimumTargetProximity) {
			Explode();
		}
		CheckForDeadMissile ();
	}

	// Sometimes the missile will miss it's target and keep going. If it does, kill after a certain amount of time.
	private void CheckForDeadMissile()
	{
		TimeSinceStart += Time.deltaTime;
		if (TimeSinceStart >= MAX_TIME_TO_LIVE) {
			Destroy ();
		}
	}
		
	private void Explode()
	{
		Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
		Destroy();
	}

	private void Destroy()
	{
		Destroy (TargetReticuleInstance);
		Destroy (gameObject);
	}
}
