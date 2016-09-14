using UnityEngine;
using System.Collections;
using System;

public class InputController : MonoBehaviour 
{
	public GameObject ExplosionPrefab;

    void Start()
    {
		
    }

	void Update () 
	{
		// Native android touch events
		foreach (Touch touch in Input.touches) {
			HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
		}

		// Simulated touch events from mouse events
		if (Input.touchCount == 0) {
			if (Input.GetMouseButtonDown(0) ) {
				HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
			} else if (Input.GetMouseButton(0) ) {
				HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
			} else if (Input.GetMouseButtonUp(0) ) {
				HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
			}
		}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) 
	{
		switch (touchPhase) {
		case TouchPhase.Began:
			touchPosition.z = 0;
			Instantiate (ExplosionPrefab, touchPosition, Quaternion.Euler(0,180,0));
			break;
		case TouchPhase.Moved:
			// TODO
			break;
		case TouchPhase.Ended:
			// TODO
			break;
		}
	}
}
