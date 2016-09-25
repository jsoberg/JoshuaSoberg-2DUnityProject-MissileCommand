using UnityEngine;
using System.Collections;

public abstract class MissileController : MonoBehaviour 
{
	public GameObject TargetReticule;
	private Object TargetReticuleInstance;
	public GameObject ExplosionPrefab;

	public float MinimumTargetProximity = 1;
	public float InitialSpeed = 1;
	public float AcceleratePerSecond = 1;
	public Vector2 TargetPosition;
	protected Vector3 StartPosition;

	// 3 Seconds
	public bool CheckTimeToLive;
	private const float MAX_TIME_TO_LIVE = 3;
	private float TimeSinceStart;

	void Start () 
	{
        PreStart();

		StartPosition = transform.position;
		StartTargetAnimation ();

		// Start heading toward target.
		Vector2 direction = Vector2.zero;
		direction.x = (TargetPosition.x - transform.position.x);
		direction.y = (TargetPosition.y - transform.position.y);
		GetComponent<Rigidbody2D> ().AddRelativeForce (direction.normalized * InitialSpeed, ForceMode2D.Force);
		RotateToTarget();

        PostStart();
	}

    protected virtual void PreStart()
    {

    }

    protected virtual void PostStart()
    {

    }

	void RotateToTarget()
	{
		float diffX = TargetPosition.x - transform.position.x;
		float diffY = TargetPosition.y - transform.position.y;

		float rotationZ = Mathf.Atan2(diffY, diffX) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotationZ - 90);
	}

	void StartTargetAnimation()
	{
		if (TargetReticule != null) {
			TargetReticuleInstance = Instantiate (TargetReticule, TargetPosition, Quaternion.identity);
		}
	}

	void Update () 
	{
		GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + (GetComponent<Rigidbody2D>().velocity * AcceleratePerSecond * Time.deltaTime);

		if (HasReachedTarget()) {
			Explode();
		}

		if (CheckTimeToLive) {
			CheckForDeadMissile ();
		}
	}

	protected virtual bool HasReachedTarget()
	{
		return (Vector2.Distance (transform.position, TargetPosition) < MinimumTargetProximity);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Explode();
        CollidedWith(coll);
	}

    protected virtual void CollidedWith(Collision2D coll)
    {

    }

    // Sometimes the missile will miss it's target and keep going. If it does, kill after a certain amount of time.
    private void CheckForDeadMissile()
	{
		TimeSinceStart += Time.deltaTime;
		if (TimeSinceStart >= MAX_TIME_TO_LIVE) {
			Destroy ();
		}
	}

	protected void Explode()
	{
		Instantiate (ExplosionPrefab, transform.position, Quaternion.identity);
		Destroy();
	}

	private void Destroy()
	{
		if (TargetReticuleInstance != null) {
			Destroy (TargetReticuleInstance);
		}
		Destroy (gameObject);
	}
}