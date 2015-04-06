using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

public abstract class Drink : MonoBehaviour {

	// Cache for prefabs
	private static Dictionary<string, GameObject> sPrefabs = new Dictionary<string, GameObject>();
		
    void Start() {
        // Debug.Log(GetType().Name);
    }

	public string GetName() {
		return GetType ().Name;
	}

	public static string GetPrefabName(string name) {
		string prefabName = Regex.Replace(
			name,
			"(?<=.)([A-Z])", 
			"_$0",
			RegexOptions.Compiled
		).ToLower ();

		return "Prefabs/Drinks/" + prefabName;
	}

	public static GameObject GetPrefab(string name) {
		if (sPrefabs.ContainsKey (name))
			return sPrefabs [name];

		GameObject prefab = Resources.Load (GetPrefabName (name), typeof(GameObject)) as GameObject;

		sPrefabs.Add (name, prefab);

		return prefab;
	}

    public bool SameAs(Drink drink) {
        return this.GetType().Equals(drink.GetType());
    }

}
