using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainCanvasManager : MonoBehaviourSingleton<MainCanvasManager> {

    public GameObject gamePausedPanel;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePausedPanel != null && gamePausedPanel.activeSelf != true) {
                MainEventManager.Instance.PauseGameEventInvoke();
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
