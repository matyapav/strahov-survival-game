using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainCanvasManager : MonoBehaviourSingleton<MainCanvasManager> {

    public GameObject gamePausedPanel;

    private void OnEnable()
    {
        MainEventManager.Instance.GamePauseEvent.AddListener(PauseGame);
        MainEventManager.Instance.GameResumeEvent.AddListener(ResumeGame);
    }

    private void OnDisable()
    {
        MainEventManager.Instance.GamePauseEvent.RemoveListener(PauseGame);
        MainEventManager.Instance.GameResumeEvent.RemoveListener(ResumeGame);
    }

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
