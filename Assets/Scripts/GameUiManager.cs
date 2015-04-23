using UnityEngine;
using System.Collections;

public class GameUiManager : MonoBehaviour {

	public GameStateManager stateManager;

	public void PauseOrResumeGame() {
        if (stateManager)
			stateManager.PauseOrResumeGame ();
    }

	public void Pause() {
		if (stateManager)
			stateManager.Pause ();
	}

	public void Resume() {
		if (stateManager)
			stateManager.Resume ();
	}

	public void TryAgain() {
		if (stateManager) 
			stateManager.ResetGame ();
	}

	public void Quit() {
		if (stateManager)
			stateManager.Quit ();
	}
}
