using System;
using UnityEngine;
using TouchScript.Gestures;
using System.Collections.Generic;

public class TrayHandler : MonoBehaviour {

	private Order mOrder;
	private Dictionary<string, GameObject> mDrinkGameObjects = new Dictionary<string, GameObject> ();

	public void Start() 
	{
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
		mOrder = Order.CreateOrder (
			"BeerBottle", 
			"WineGlass",
			"CocktailMartini"
		);

		UpdateOrderStatus ();
    }

    public void AddDrink(Drink drink) {
		if (!mOrder.IsCompleted ()) {
			if (mOrder.AddDrink(drink)) {
				// Added successfully
				Debug.Log ("[AddDrink] Added " + drink.GetName());
			}
		}

        UpdateOrderStatus ();
    }

    private void UpdateOrderStatus() {
		Debug.Log ("[UpdateOrderStatus] Is completed? " + mOrder.IsCompleted());
		
		Dictionary<string, int> expectedDrinks = mOrder.GetDrinks ();
		Dictionary<string, int> actualDrinks = mOrder.GetAddedDrinks ();

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
				SpriteRenderer renderer = drink.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);

				mDrinkGameObjects.Add (name, drink);

				// Increase the iteration
				index++;
			}
		}

    }

}
