using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuUIManager : MonoBehaviour {

    #region Start button
	
	public void OnStartPressed() {
		Application.LoadLevel("GameScene");
    }

    #endregion

    #region Options button

    public void OnOptionsPressed() {
    }

    #endregion

    #region Help button

    public void OnHelpPressed() {
    }

    #endregion

    #region Scores button

	public void OnScoresPressed() {
	}

    #endregion

}
