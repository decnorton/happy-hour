using System;
using TouchScript.Gestures;
using TouchScript.Hit;
using UnityEngine;
using Random = UnityEngine.Random;

public class TapSpawner : MonoBehaviour {

    public Transform cubePrefab;
    public Transform container;
    public float scale = .5f;

	private void OnEnable() {
        // Subscribe
        GetComponent<TapGesture>().Tapped += tappedHandler;
    }

    private void OnDisable() {
        // Unsubscribe
        GetComponent<TapGesture>().Tapped -= tappedHandler;
    }

    private void tappedHandler(object sender, EventArgs e) {
        var gesture = sender as TapGesture;
        ITouchHit hit;

        gesture.GetTargetHitResult(out hit);

        var hit3d = hit as ITouchHit3D;

        if (hit3d == null)
            return;

        Color color = new Color(Random.value, Random.value, Random.value);

        var cube = Instantiate(cubePrefab) as Transform;

        cube.parent = container;
        cube.name = "Cube";
        cube.localScale = Vector3.one * scale * cube.localScale.x;
        cube.position = hit3d.Point + hit3d.Normal * 2;
        cube.renderer.material.color = color;
    }
}
