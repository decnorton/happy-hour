using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameStateManager : MonoBehaviour {

	// Score
	private int score = 0;

	// Warnings
	private int warnings = 0;
	
	public GameObject gameOverPanel;
	public GameObject pausePanel;

	public Image scoreOne;
	public Image scoreTwo;
	public Image scoreThree;

	public GameTimer timer;
	public Text pauseResumeLabel;
	
	public AudioSource music;
	public AudioSource ambient;

	// Use this for initialization
	void Start () {
		if (timer)
			timer.StartTimer();
	}
	
	// Update is called once per frame
	void Update () {

		if (score > 0) {
			Image scoreImage;

			if (score <= 40) {
				scoreImage = scoreOne;
			} else if (score <= 80) {
				scoreImage = scoreTwo;
			} else {
				scoreImage = scoreThree;
			}

			float amount;

			if (score == 0) {
				amount = 0;
			} else if (score % 40 == 0) {
				amount = 1;
			} else {
				amount = (score % 40) / 40f;
			}

			Debug.Log ("Score: " + score + " | Fill amount: " + amount);

			if (scoreImage) {
				scoreImage.fillAmount = amount;
			}
		}

	}

	public void IncrementScore (int amount) {
		score += amount;
	}

	public void IncrementWarnings(int amount) {
		warnings += amount;

		if (warnings >= 3) {
			GameOver();
		}
	}

	public void PauseOrResumeGame() {
		Debug.Log("PAUSE GODDAMNIT");
		
		if (timer) {
			if (timer.IsRunning()) {
				Pause();
			} else {
				Resume();
			}
		}
	}

	public void Pause() {
		timer.Pause();
		pauseResumeLabel.text = "RESUME";

		if (pausePanel) {
			pausePanel.SetActive (true);
		}
	}

	public void Resume() {
		timer.StartTimer();
		pauseResumeLabel.text = "PAUSE";
		
		if (pausePanel) {
			pausePanel.SetActive (false);
		}
	}

	public void Quit() {
		Application.LoadLevel ("MenuScene");
	}

	public void GameOver() {
		timer.Pause ();

		if (gameOverPanel) {
			gameOverPanel.SetActive(true);
		}

		if (ambient)
			ambient.Stop ();

		if (music)
			music.Stop ();
	}

	public void ResetGame() {
		Application.LoadLevel (Application.loadedLevel);
	}
}
