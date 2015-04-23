using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour {

	public GameStateManager stateManager;
	public TrayHandler tray;

	private GameObject mCustomerGameObject;
	private Customer mCustomer;
	private bool mCustomerHasReachedBar = false;

	// Use this for initialization
	void Start () {
		SpawnCustomer ();
	}

	// Update is called once per frame
	void Update () {
		if (mCustomerGameObject) {
			if (mCustomerGameObject.transform.position.y < -4) {
				mCustomerGameObject.transform.position -= new Vector3(0, Time.deltaTime * -2);
			}

			if (!mCustomerHasReachedBar && mCustomerGameObject.transform.position.y >= -4) {
				Debug.Log ("Reached the bar");

				mCustomerHasReachedBar = true;

				mCustomer.StartTimer();

				// Tell tray to generate order
				if (tray) {
					tray.GenerateOrder(mCustomer);
				}
			}
		}
	}

	private GameObject GetRandomCustomer() {
        // Get random name
        string[] customers = Customer.GetAvailableCustomers();

		int index = Random.Range (0, customers.Length - 1);

		return Customer.GetPrefab(customers[index]);
	}

	public void ResetCustomer() {
		
		if (mCustomerGameObject) {
			Destroy (mCustomerGameObject);
		}
		
		mCustomer = null;
		mCustomerGameObject = null;
		
		mCustomerHasReachedBar = false;
	}


	public void SpawnCustomer() {
		StartCoroutine (SpawnCustomerCoroutine ());
	}

	public IEnumerator SpawnCustomerCoroutine() {
		Debug.Log ("[SpawnCustomer]");

		ResetCustomer ();

		yield return new WaitForSeconds (Random.Range(0.2f, 5f));

		// Create a new instance
		mCustomerGameObject = Instantiate (
			GetRandomCustomer(),
			transform.position,
			transform.rotation
		) as GameObject;

		mCustomer = mCustomerGameObject.GetComponent<Customer>();

		// Set the parent
		mCustomerGameObject.transform.parent = transform;

		// Push them off the screen
		mCustomerGameObject.transform.position -= new Vector3 (0, 4);

		// Set the sorting layer
		SpriteRenderer spriteRenderer = mCustomerGameObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sortingLayerName = "Customers";

		if (mCustomerGameObject) {
			Debug.Log ("Got customer");
		}
	}

	public void RunOutOfTime() {
		Destroy (mCustomerGameObject);

		tray.Reset ();

		stateManager.IncrementWarnings (1);
	}

}
