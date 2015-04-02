using System;
using UnityEngine;
using TouchScript.Gestures;

public class TrayHandler : MonoBehaviour {

    public void AddDrink(Drink drink) {
        Debug.Log("Added " + drink.GetType().Name);
        BuildOrder(drink);
    }

    private void BuildOrder(Drink drink) {

    }

}
