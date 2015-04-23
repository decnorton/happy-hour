using UnityEngine;
using System.Collections;

public class BarHandler : MonoBehaviour {

	TrayHandler[] trays;

	// Use this for initialization
	void Start () {
		trays = FindObjectsOfType<TrayHandler> ();

		Debug.Log (trays.Length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
