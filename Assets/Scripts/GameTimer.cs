using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

    float currentTime = 0f;

    /**
     * State
     */
    bool isRunning = false;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
        if (isRunning) {
            currentTime += Time.deltaTime;
        }
    }

    public bool IsRunning() {
        return isRunning;
    }

    public void StartTimer() {
		isRunning = true;
		Time.timeScale = 1;
    }

    public void Pause() {
        isRunning = false;
		Time.timeScale = 0;
    }

    public void Reset() {
        currentTime = 0f;
        isRunning = false;
    }


}
