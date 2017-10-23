using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MainCanvasManager : MonoBehaviourSingletonPersistent<MainCanvasManager> {

    public GameObject gamePausedPanel;

    public UnityEvent PauseAction;
    public UnityEvent ResumeAction;

    private void OnEnable()
    {
        PauseAction.AddListener(PauseGame);
        ResumeAction.AddListener(ResumeGame);
    }

    private void OnDisable()
    {
        PauseAction.RemoveListener(PauseGame);
        ResumeAction.RemoveListener(ResumeGame);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (gamePausedPanel != null && gamePausedPanel.activeSelf != true) {
                PauseAction.Invoke();
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
