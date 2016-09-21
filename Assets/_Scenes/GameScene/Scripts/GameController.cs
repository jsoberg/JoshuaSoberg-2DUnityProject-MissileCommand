using UnityEngine;
using System;
using System.Collections;

public class GameController : MonoBehaviour 
{
	private const int FRAMES_TO_NEXT_MISSILE = 100;

	public Rigidbody2D EnemyMissilePrefab;

	private int FramesSinceLastMissile;

	private readonly System.Random MyRandom = new System.Random();

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

	private void CreateNewEnemyMissile()
	{
		Vector2 start = GetPositionWithRandomX(50);
		Rigidbody2D missileClone = (Rigidbody2D)Instantiate (EnemyMissilePrefab, start, Quaternion.identity);
		Vector2 end = GetPositionWithRandomX(-35);
		missileClone.GetComponent<MissileController> ().TargetPosition = end;
	}

	private Vector2 GetPositionWithRandomX(int yVal)
	{
		int startX = MyRandom.Next(100) - 50;
		return new Vector2(startX, yVal);
	}
}
