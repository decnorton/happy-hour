using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public Color defaultColor;
    public Color pressedColor;
    private Material material;

    void Start() {
        material = renderer.material;
    }

    void OnTouchDown() {
        material.color = pressedColor;
    }

    void OnTouchUp() {
        material.color = defaultColor;
    }

    void OnTouchStay() {
        material.color = pressedColor;
    }

    void OnTouchExit() {
        material.color = defaultColor;
    }

}
