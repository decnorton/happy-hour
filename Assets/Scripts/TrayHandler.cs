using System;
using UnityEngine;
using TouchScript.Gestures;

public class TrayHandler : MonoBehaviour {

    private void OnEnable() {
        GetComponent<ReleaseGesture>().Released += onRelease;
    }

    private void OnDisable() {
        GetComponent<ReleaseGesture>().Released -= onRelease;
    }

    private void onRelease(object sender, EventArgs e) {
        Debug.Log("onRelease");
    }

}
