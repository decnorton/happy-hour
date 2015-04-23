using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public abstract class Customer : MonoBehaviour {

	private const int TIMER_MAX = 15; // Seconds

	// Timer prefab
	private static GameObject sTimer = Resources.Load ("Prefabs/customer_timer", typeof(GameObject)) as GameObject;

	private GameObject timerGameObject;
	private CustomerTimer timer;
	private CustomerSpawner spawner;

	// Timer state
	private bool isTimerRunning = false;
	private float currentTime = 0;

	// Cache for prefabs
	private static Dictionary<string, GameObject> sPrefabs = new Dictionary<string, GameObject>();

	// Available customers
	public static string[] sCustomers = {
		"female_dress_1",
		"female_dress_2",
		"female_dress_3",
		"female_dress_4",
		"male_boy_1",
		"male_boy_2",
		"male_boy_3",
		"male_fat_1",
		"male_fat_2",
		"male_normal_1",
		"male_normal_2",
		"male_normal_3",
		"male_suit"
	};

	// Use this for initialization
	void Start () {
		InstantiateTimer ();

		spawner = transform.parent.GetComponent<CustomerSpawner> ();
	}

	// Update is called once per frame
	void Update () {
		if (isTimerRunning) {
			currentTime += Time.deltaTime;

			if (timer) {
				float progress = (currentTime / TIMER_MAX) * 100;

				if (progress >= 100) {
					if (spawner) {
						spawner.RunOutOfTime();
					}
				} else {
					timer.SetProgress (progress);
				}
			}
		}
	}

	private void InstantiateTimer() {
		timerGameObject = Instantiate (
			sTimer,
			transform.position,
			transform.rotation
		) as GameObject;

		timer = timerGameObject.GetComponent<CustomerTimer> ();

		timerGameObject.transform.parent = transform;
		timerGameObject.transform.localPosition = new Vector3 (0, 2, -1);
	}

	public void StartTimer() {
		isTimerRunning = true;
	}

	public void StopTimer() {
		isTimerRunning = false;
	}

	public static string[] GetAvailableCustomers() {
		return sCustomers;
	}

	public static string GetPrefabPath(string prefabName) {
		return "Prefabs/Customers/" + prefabName;
	}

	public static GameObject GetPrefab(string name) {
		Debug.Log (name);

		if (sPrefabs.ContainsKey (name))
			return sPrefabs [name];

		GameObject prefab = Resources.Load (GetPrefabPath (name), typeof(GameObject)) as GameObject;

		sPrefabs.Add (name, prefab);

		return prefab;
	}
}
