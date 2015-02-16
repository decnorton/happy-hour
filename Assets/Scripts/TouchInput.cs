using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchInput : MonoBehaviour {

    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;

    private RaycastHit hit;

	// Update is called once per frame
	void Update () {
        HandleMouse();
        HandleTouches();
	}

    bool IsMobile() {
        #if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8
            return true;
        #else
            return false;
        #endif
    }

    void HandleMouse() {
        // Ignore this for mobile
        if (IsMobile())
            return;

        if (!Input.GetMouseButton(0) && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonUp(0))
            return;

        // Copy old touchList to touchesOld
        touchesOld = new GameObject[touchList.Count];
        touchList.CopyTo(touchesOld);
        touchList.Clear();

        // Figure out where the mouse is pointing
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        // Check if the ray hits any objects
        if (Physics.Raycast(ray, out hit, touchInputMask)) {
            GameObject recipient = hit.transform.gameObject;

            // Add to touchList
            touchList.Add(recipient);

            // Emulate touch events
            if (Input.GetMouseButton(0)) {
                SendTouchMessage(recipient, "OnTouchStay", hit.point);
            }

            if (Input.GetMouseButtonDown(0)) {
                SendTouchMessage(recipient, "OnTouchDown", hit.point);
            }

            if (Input.GetMouseButtonUp(0)) {
                SendTouchMessage(recipient, "OnTouchUp", hit.point);
            }
        }

        // If any of the touches from the last frame aren't present in the
        // current one then send the exit message.
        foreach (GameObject gameObject in touchesOld) {
            if (!touchList.Contains(gameObject)) {
                SendTouchMessage(gameObject, "OnTouchExit", hit.point);
            }
        }
    }

    void HandleTouches() {
        // Quit early if there aren't any touches
        if (Input.touchCount == 0)
            return;

        // Copy old touch list to touchesOld
        touchesOld = new GameObject[touchList.Count];
        touchList.CopyTo(touchesOld);
        touchList.Clear();

        // Loop through each touch
        foreach (Touch touch in Input.touches) {
            // Figure out where the touch is pointing from the camera
            Ray ray = camera.ScreenPointToRay(touch.position);

            // Figure out if it intercepts with any objects
            if (Physics.Raycast(ray, out hit, touchInputMask)) {
                GameObject recipient = hit.transform.gameObject;

                // Add to the list of current touches
                touchList.Add(recipient);

                // Send touch message
                switch (touch.phase) {
                    case TouchPhase.Began:      SendTouchMessage(recipient, "OnTouchDown", hit.point); break;
                    case TouchPhase.Ended:      SendTouchMessage(recipient, "OnTouchUp",   hit.point); break;
                    case TouchPhase.Stationary: SendTouchMessage(recipient, "OnTouchStay", hit.point); break;
                    case TouchPhase.Moved:      SendTouchMessage(recipient, "OnTouchStay", hit.point); break;
                    case TouchPhase.Canceled:   SendTouchMessage(recipient, "OnTouchExit", hit.point); break;
                }
            }
        }

        // If any of the touches from the last frame aren't present in the
        // current one then send the exit message.
        foreach (GameObject gameObject in touchesOld) {
            if (!touchList.Contains(gameObject)) {
                SendTouchMessage(gameObject, "OnTouchExit", hit.point);
            }
        }
    }

    void SendTouchMessage(GameObject gameObject, string methodName, Vector3 point) {
        gameObject.SendMessage(methodName, point, SendMessageOptions.DontRequireReceiver);
    }

}
