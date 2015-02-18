using System;
using UnityEngine;
using TouchScript.Gestures.Simple;

public class DragHandler : MonoBehaviour {

    public LayerMask layerMask;

    private Vector3 startPosition;


    private void Awake() {
        startPosition = transform.localPosition;
    }

    private void OnEnable() {
        GetComponent<SimplePanGesture>().PanStarted += onPanStarted;
        GetComponent<SimplePanGesture>().Panned += onPanned;
        GetComponent<SimplePanGesture>().PanCompleted += onPanCompleted;
    }

    private void OnDisable() {
        GetComponent<SimplePanGesture>().PanStarted -= onPanStarted;
        GetComponent<SimplePanGesture>().Panned -= onPanned;
        GetComponent<SimplePanGesture>().PanCompleted -= onPanCompleted;
    }

    private void onPanStarted(object sender, EventArgs e) {
        transform.localPosition = new Vector3(
            startPosition.x,
            startPosition.y,
            startPosition.z - 1
        );
    }

    private void onPanned(object sender, EventArgs e) {

    }

    private void onPanCompleted(object sender, EventArgs e) {
        transform.localPosition = startPosition;

        SimplePanGesture gesture = (SimplePanGesture) sender;

        // Debug.Log(gesture.ScreenPosition);

        // Get the main camera
        Camera camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(gesture.ScreenPosition);

        RaycastHit2D hit = Physics2D.GetRayIntersection(
            ray,
            Mathf.Infinity,
            layerMask
        );

        Debug.Log(hit);

        if (hit != null) {
            Debug.Log(hit.transform);
        }
    }

}