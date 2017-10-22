using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasManager : MonoBehaviour {

    public GameObject gamePausedPanel;
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePausedPanel != null && gamePausedPanel.activeSelf != true) {
                gamePausedPanel.SetActive(true);
                PauseGame();
            }
        }
	}

    public void PauseGame() {
        Time.timeScale = 0f;
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
    }
}
