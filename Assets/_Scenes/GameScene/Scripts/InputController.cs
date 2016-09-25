using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class InputController : MonoBehaviour 
{
	public const int MOUSE_FINGER_ID = -1;
	
	public EventSystem UiEventSystem;
    public HUDInventoryController HudInventoryController;

    public SiloController CenterSilo;
    public SiloController LeftSilo;
    public SiloController RightSilo;
    private SiloController[] AllSilos;

    void Start()
    {
        CenterSilo = CenterSilo.GetComponent<SiloController>();
        LeftSilo = LeftSilo.GetComponent<SiloController>();
        RightSilo = RightSilo.GetComponent<SiloController>();

        AllSilos = new SiloController[] { CenterSilo, LeftSilo, RightSilo };
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
        if(!HudInventoryController.CanFireMissile()) {
            HudInventoryController.MissileFireAttempted();
            return;
        }

        int closest = 0;
        float closestDistance = float.MaxValue; 
        for (int i = 0; i < AllSilos.Length; i++) {
            if (AllSilos[i].IsDestroyed) {
                continue;
            }

            float distance = Vector2.Distance(AllSilos[i].gameObject.transform.position, touchPosition);
            if (distance < closestDistance) {
                closestDistance = distance;
                closest = i; 
            }
        }

        AllSilos[closest].FireMissile(touchPosition);
        HudInventoryController.MissileFired();

    }
}
