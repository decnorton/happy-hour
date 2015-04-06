using System;
using UnityEngine;
using TouchScript.Gestures;
using System.Collections.Generic;

class Order
{
	// TODO: Change to List and don't accept duplicate drinks
	Dictionary<string, int> mDrinks = new Dictionary<string, int>();
	Dictionary<string, int> mAddedDrinks = new Dictionary<string, int>();

	private Order (string[] drinks) 
	{
		foreach (string drink in drinks) {
			if (!mDrinks.ContainsKey(drink)) {
				mDrinks.Add(drink, 1);
			} else {
				mDrinks[drink]++;
			}
		}
	}

	public Dictionary<string, int> GetDrinks () {
		return mDrinks;
	}

	public Dictionary<string, int> GetAddedDrinks() {
		return mAddedDrinks;
	}
	
	public bool AddDrink(Drink drink) {
		string drinkName = drink.GetName();

		Debug.Log ("Drink name: " + drinkName);

		if (mDrinks.ContainsKey (drinkName)) {
			if (!mAddedDrinks.ContainsKey(drinkName)) {
				mAddedDrinks.Add(drinkName, 1);
			} else if (mDrinks[drinkName] > mAddedDrinks[drinkName]) {
				mAddedDrinks[drinkName]++;
			} else {
				// We only get to here if we haven't added a new drink or increased the counter on an existing
				return false;
			}

			return true;
		}

		return false;
	}

	public bool IsCompleted() {
		foreach (string drinkName in mDrinks.Keys) {
			if (!mAddedDrinks.ContainsKey(drinkName))
				// The drink hasn't even beed added yet
				return false;

			if (mAddedDrinks[drinkName] < mDrinks[drinkName])
				return false;

		}

		return true;
	}

	public static Order CreateOrder(params string[] drinks) 
	{
		return new Order(drinks);
	}
}

