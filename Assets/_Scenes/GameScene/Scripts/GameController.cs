using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour 
{
	private const int FRAMES_TO_NEXT_MISSILE = 100;

	public Rigidbody2D EnemyMissilePrefab;

	private int FramesSinceLastMissile;

	private readonly Random MyRandom = new Random();

	void Start () 
	{
	
	}

	void Update () 
	{
		FramesSinceLastMissile ++;
		if (FramesSinceLastMissile >= FRAMES_TO_NEXT_MISSILE) {
			CreateNewEnemyMissile ();
			FramesSinceLastMissile = 0;
		}
	}

	void CreateNewEnemyMissile()
	{
		Vector2 start = new Vector2(0, 50);
		Rigidbody2D missileClone = (Rigidbody2D)Instantiate (EnemyMissilePrefab, start, Quaternion.identity);
		Vector2 end = new Vector2(0, -35);
		missileClone.GetComponent<MissileMovement> ().TargetPosition = end;
	}
}
