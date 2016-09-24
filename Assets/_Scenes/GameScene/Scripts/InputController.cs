using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class InputController : MonoBehaviour 
{
	public const int MOUSE_FINGER_ID = -1;
	
	public EventSystem UiEventSystem;

    public SiloController CenterSilo;

    void Start()
    {
        CenterSilo = CenterSilo.GetComponent<SiloController>();
    }

	void Update () 
	{
		// Native android touch events
		foreach (Touch touch in Input.touches) {
			// Ignore if we're over the UI Event system.
			if (UiEventSystem.IsPointerOverGameObject (touch.fingerId)) {
				continue;
			}

			HandleTouch(touch.fingerId, Camera.main.ScreenToWorldPoint(touch.position), touch.phase);
		}

		// Simulated touch events from mouse events.
		if (Input.touchCount == 0 && !UiEventSystem.IsPointerOverGameObject (MOUSE_FINGER_ID)) {
			if (Input.GetMouseButtonDown(0) ) {
				HandleTouch(MOUSE_FINGER_ID, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Began);
			} else if (Input.GetMouseButton(0) ) {
				HandleTouch(MOUSE_FINGER_ID, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
			} else if (Input.GetMouseButtonUp(0) ) {
				HandleTouch(MOUSE_FINGER_ID, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
			}
		}
	}

	private void HandleTouch(int touchFingerId, Vector3 touchPosition, TouchPhase touchPhase) 
	{
		switch (touchPhase) {
		case TouchPhase.Began:
				LaunchMissile (touchPosition);
				break;
			case TouchPhase.Moved:
				// TODO
				break;
			case TouchPhase.Ended:
				// TODO
				break;
		}
	}

	private void LaunchMissile(Vector3 touchPosition)
	{
        CenterSilo.FireMissile(touchPosition);
	}
}
