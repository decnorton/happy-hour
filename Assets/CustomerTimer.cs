using UnityEngine;
using System.Collections;

public class CustomerTimer : MonoBehaviour {

	private float progress = 0;

	private Material material;

	// Use this for initialization
	void Start () {
		material = GetComponent<Renderer>().material; 
	}
	
	// Update is called once per frame
	void Update () {
		material.SetFloat ("_Cutoff", Mathf.InverseLerp (100, 0, progress));
	}

	public void SetProgress(float progress) {
		this.progress = Mathf.Min (100, progress);
	}

}
