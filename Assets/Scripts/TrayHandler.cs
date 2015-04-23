using System;
using UnityEngine;
using TouchScript.Gestures;
using System.Collections.Generic;

public class TrayHandler : MonoBehaviour {

	public GameStateManager stateManager;
	public CustomerSpawner customerSpawner;

	private Order mOrder;
	private Customer mCurrentCustomer;

	private Dictionary<string, GameObject> mDrinkGameObjects = new Dictionary<string, GameObject> ();

	public void Start() {
	}

	public void GenerateOrder(Customer customer) {
		Debug.Log ("[GenerateOrder]");

		if (!customer) {
			Debug.LogError ("GameObject doesn't have customer component");
			return;
		}

		// Set the current customer
		mCurrentCustomer = customer;
	
		// Build the order
		BuildRandomOrder ();
	}

    public void BuildRandomOrder() {
		// Destroy each GameObject
		foreach (GameObject gameObject in mDrinkGameObjects.Values) {
			Destroy(gameObject);
		}

		// Clear the Dictionary
		mDrinkGameObjects.Clear ();

		// Create a new order
		// TODO: Select random drinks

		string[] availableDrinks = Drink.GetAvailableDrinks ();
		List<string> drinks = new List<string>();

		for (int i = 0; i < 3; i++) {
			string drinkName = null;

			while (drinkName == null) {
				int index = UnityEngine.Random.Range(0, availableDrinks.Length);

				drinkName = availableDrinks[index];

				if (drinks.Contains(drinkName)) {
					drinkName = null;
				}
			}

			drinks.Add(drinkName);
		}

		mOrder = Order.CreateOrder (drinks.ToArray());

		UpdateOrderStatus ();
    }

    public void AddDrink(Drink drink) {
		if (mOrder != null && !mOrder.IsCompleted ()) {
			if (mOrder.AddDrink(drink)) {
				// Added successfully
				Debug.Log ("[AddDrink] Added " + drink.GetName());
			}
		}

        UpdateOrderStatus ();
    }

    private void UpdateOrderStatus() {
		if (mOrder == null) {
			Debug.Log("Order is null");
			return;
		}

		Debug.Log ("[UpdateOrderStatus] Is completed? " + mOrder.IsCompleted());

		Dictionary<string, int> expectedDrinks = mOrder.GetDrinks ();
		Dictionary<string, int> actualDrinks = mOrder.GetAddedDrinks ();

		// Only if drinks have been added
		if (actualDrinks.Keys.Count > 0) {
			foreach (string name in actualDrinks.Keys) {
				if (mDrinkGameObjects.ContainsKey(name)) {
					GameObject prefab = mDrinkGameObjects[name];

					prefab.GetComponent<Drink>().PlaySound();
					
					if (prefab) {
						prefab.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1);
					}
				}
			}
		}

		if (mOrder.IsCompleted ()) {
			OnOrderCompleted();
			return;
		}

		// Show ghost drinks
		if (mDrinkGameObjects.Count < expectedDrinks.Count) {

			int index = 0; // Keep track of how many we've added
			float spacing = 1.3f; // Spacing between each drink on the tray

			foreach (string name in expectedDrinks.Keys) {
				// Create a new instance
				GameObject drink = Instantiate (
					Drink.GetPrefab(name),
					transform.position,
					transform.rotation
				) as GameObject;

				// Set the parent to the tray
				drink.transform.parent = transform;

				// Scale it down
				drink.transform.localScale = new Vector3(0.4f, 0.4f, 0.5f);

				// Reposition it a bit
				drink.transform.position += new Vector3(
					// Space each one out
					-1.4f + (index * spacing),

					// Push up a bit
					0.2f
				);

				// Change opacity
				SpriteRenderer spriteRenderer = drink.GetComponent<SpriteRenderer>();
				spriteRenderer.color = new Color(1, 1, 1, 0.4f);
				spriteRenderer.sortingLayerName = "Bar";
				spriteRenderer.sortingOrder = 10;

				mDrinkGameObjects.Add (name, drink);

				// Increase the iteration
				index++;
			}
		}
    }

	private void OnOrderCompleted() {
		// Order has been completed

		// 1: Increment score
		// 2: Reset tray

		stateManager.IncrementScore (10);

		Reset ();
	}

	public void Reset() {
		foreach (GameObject drinkGameObject in mDrinkGameObjects.Values) {
			Destroy(drinkGameObject);
		}
		
		mOrder = null;
		mCurrentCustomer = null;
		
		if (customerSpawner) {
			customerSpawner.SpawnCustomer();
		}
	}

	public bool IsOrderInProgress() {
		return mOrder != null;
	}

}
